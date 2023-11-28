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
        private ListObject? listObject = null;
        private int _itemsEachPage;

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
            listObject = new(list, selectMenu);
        }

        public void RunMenu(string title, Action previousMenu)
        {
            int currentPage = 0;
            List<ConsoleKey> allowedKeys = new();
            List<Model>? pageOfList = CreatePageOfList(listObject?.List, currentPage);
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;

            while (selectedMenu == null)
            {
                PrintMenu(title, pageOfList);

                allowedKeys = CreateListOfAllowedKeys(currentPage, pageOfList.Count, listObject?.List);
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
                    currentPage--;
                    _selectedMenuIndex = 0;
                    pageOfList = CreatePageOfList(listObject?.List, currentPage);
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    currentPage++;
                    _selectedMenuIndex = 0;
                    pageOfList = CreatePageOfList(listObject?.List, currentPage);
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
                    listObject = new(listObject?.List, null);
                    break;
                }
            }

            nextMethod = selectedMenu ?? listObject?.Method ?? previousMenu;
            listObject = new(null, listObject?.Method);

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

            Console.WriteLine(_menuObjects.Count + list?.Count);

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
        }

        private List<Model> CreatePageOfList(List<Model>? list, int page)
        {
            List<Model> pageOfList = new();
            int startPageIndex = page * _itemsEachPage;

            for (int i = startPageIndex; i < list?.Count && i < startPageIndex + _itemsEachPage; i++)
            {
                pageOfList.Add(list[i]);
            }

            return pageOfList;
        }

        private List<ConsoleKey> CreateListOfAllowedKeys(int currentPage, int pageOfListCount = 0, List<Model>? list = null)
        {
            int numberOfPages = (int)Math.Ceiling((list?.Count ?? 0) / 10.0);
            List<ConsoleKey> allowedKeys = new();

            if (_selectedMenuIndex > 0)
            {
                allowedKeys.Add(ConsoleKey.UpArrow);
            }
            if (_selectedMenuIndex < (_menuObjects.Count + pageOfListCount) - 1)
            {
                allowedKeys.Add(ConsoleKey.DownArrow);
            }

            if (list?.Count > 0 && currentPage > 0)
            {
                allowedKeys.Add(ConsoleKey.LeftArrow);
            }

            if (list?.Count > 0 && currentPage < numberOfPages - 1)
            {
                allowedKeys.Add(ConsoleKey.RightArrow);
            }

            return allowedKeys;
        }
    }
}