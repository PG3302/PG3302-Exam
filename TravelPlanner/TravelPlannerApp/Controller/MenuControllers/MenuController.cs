using System.Configuration;
using TravelDatabase.Data.Log;
using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Controller.UserControllers;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class MenuController
    {
        private readonly List<MenuObject> _menuObjects = new();
        private readonly UserController _userController = new();

        private Model? _currentModel = null;
        private int _selectedMenuIndex = 0;
        private ListObject? _listObject = null;
        private int _itemsEachPage;
        private int _currentPage = 0;
        private int _numberOfPages;

        public MenuController()
        {
            SetConfigValues();
        }

        public void AddMenu(string menuText, Action menuMethod)
        {
            MenuObject menuObject = new(menuText, menuMethod);

            _menuObjects.Add(menuObject);
        }

        public void AddList(List<Model> list, Action selectMenu)
        {
            _listObject = new(list, selectMenu);
        }

        public void RunMenu(string title, Action previousMenu)
        {
            _currentPage = 0;
            List<ConsoleKey> allowedKeys = new();
            List<Model>? pageOfList = CreatePageOfList(_listObject?.List);
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;

            while (selectedMenu == null)
            {
                PrintMenu(title, pageOfList);
                Console.WriteLine(pageOfList.Count);
                allowedKeys = CreateListOfAllowedKeys(pageOfList.Count, _listObject?.List);
                keyPressed = _userController.GetUserMenuChoiceKey(allowedKeys);

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    _selectedMenuIndex--;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    _selectedMenuIndex++;
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    _currentPage--;
                    _selectedMenuIndex = 0;
                    pageOfList = CreatePageOfList(_listObject?.List);
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    _currentPage++;
                    _selectedMenuIndex = 0;
                    pageOfList = CreatePageOfList(_listObject?.List);
                }
                else if (keyPressed == ConsoleKey.Enter && _selectedMenuIndex < _menuObjects.Count) //Menu object
                {
                    selectedMenu = _menuObjects[_selectedMenuIndex].Method;
                }
                else if (keyPressed == ConsoleKey.Enter) //Model object
                {
                    _currentModel = pageOfList[_selectedMenuIndex - _menuObjects.Count];
                    break;
                }
                else if (keyPressed == ConsoleKey.Escape)
                {
                    _listObject = new(_listObject?.List, null);
                    break;
                }
            }

            nextMethod = selectedMenu ?? _listObject?.Method ?? previousMenu;
            _listObject = new(null, _listObject?.Method);

            _selectedMenuIndex = 0;
            _menuObjects.Clear();

            nextMethod();
        }

        private void SetConfigValues()
        {
            try {
                string? listItemsEachPageValue = ConfigurationManager.AppSettings["listItemsEachPage"];
                
                _itemsEachPage = int.Parse(listItemsEachPageValue ?? "");
            } catch (Exception error)
            {
                Logger.LogError("Error when reading app.config", error);
            }
        }

        private void PrintMenu(string title, List<Model>? list = null)
        {
            Console.Clear();
            Console.WriteLine(title);

            for (int i = 0; i < _menuObjects.Count + list?.Count; i++)
            {
                if (i == _menuObjects.Count && list?.Count > 0)
                {
                    Console.WriteLine("---");
                }

                if (i == _selectedMenuIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[O]");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("[ ]");
                    Console.ResetColor();
                }

                if (i < _menuObjects.Count)
                {
                    Console.WriteLine($" {_menuObjects[i].Text}");
                }
                else
                {
                    Console.WriteLine($" {list?[i - _menuObjects.Count].ToString()}");
                }
            }

            if (_listObject?.List?.Count > 0)
            {
                Console.WriteLine($"Showing {_itemsEachPage} each page. Page {_currentPage + 1} of {_numberOfPages}");
            }
        }

        private List<Model> CreatePageOfList(List<Model>? list)
        {
            List<Model> pageOfList = new();
            int startPageIndex = _currentPage * _itemsEachPage;

            for (int i = startPageIndex; i < list?.Count && i < startPageIndex + _itemsEachPage; i++)
            {
                pageOfList.Add(list[i]);
            }

            return pageOfList;
        }

        private List<ConsoleKey> CreateListOfAllowedKeys(int pageOfListCount = 0, List<Model>? list = null)
        {
            _numberOfPages = (int)Math.Ceiling((list?.Count ?? 0) / (double)_itemsEachPage);

            List<ConsoleKey> allowedKeys = new();

            if (_selectedMenuIndex > 0)
            {
                allowedKeys.Add(ConsoleKey.UpArrow);
            }
            if (_selectedMenuIndex < (_menuObjects.Count + pageOfListCount) - 1)
            {
                allowedKeys.Add(ConsoleKey.DownArrow);
            }

            if (list?.Count > 0 && _currentPage > 0)
            {
                allowedKeys.Add(ConsoleKey.LeftArrow);
            }

            if (list?.Count > 0 && _currentPage < _numberOfPages - 1)
            {
                allowedKeys.Add(ConsoleKey.RightArrow);
            }

            return allowedKeys;
        }
    }
}