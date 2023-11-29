using System.Configuration;
using TravelDatabase.Data.Log;
using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Controller.UserControllers;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class MenuController
    {
        private readonly List<MenuObject> _menuObjects = [];
        private readonly UserController _userController = new();

        private Model? _currentModel = null;
        private ListObject? _listObject = null;
        private int _selectedMenuIndex = 0;
        private int _currentPage = 0;
        private int _itemsEachPage;
        private int _numberOfPages;
        private string _currentChoice = "";

        internal MenuController()
        {
            SetConfigValues();
        }

        internal Model? GetCurrentModel()
        {
            return _currentModel;
        }

        internal string GetCurrentChoice()
        {
            return _currentChoice;
        }

        internal void ResetMenuController()
        {
            _currentModel = null;
            _selectedMenuIndex = 0;
            _currentPage = 0;
            _numberOfPages = 0;
            _currentChoice = "";
        }

        internal void AddMenu(string menuText, Action menuMethod, bool breakOut = false)
        {
            MenuObject menuObject = new(menuText, menuMethod, breakOut);

            _menuObjects.Add(menuObject);
        }

        internal void AddList(List<Model> list, Action selectMenu, bool breakOut = false)
        {
            _listObject = new(list, selectMenu, breakOut);
        }

        internal void RunMenu(string title, Action previousMenu)
        {
            _currentPage = 0;
            List<ConsoleKey> allowedKeys = [];
            List<Model>? pageOfList = CreatePageOfList();
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;
            _numberOfPages = SetNumberOfPages();
            bool breakOut = false;

            while (selectedMenu == null)
            {
                PrintMenu(title, pageOfList);
                allowedKeys = CreateListOfAllowedKeys(pageOfList.Count);
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
                    pageOfList = CreatePageOfList();
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    _currentPage++;
                    _selectedMenuIndex = 0;
                    pageOfList = CreatePageOfList();
                }
                else if (keyPressed == ConsoleKey.Enter && _selectedMenuIndex < _menuObjects.Count) //Menu object
                {
                    selectedMenu = _menuObjects[_selectedMenuIndex].Method;
                    _currentChoice = _menuObjects[_selectedMenuIndex].Text;
                    breakOut = _menuObjects[_selectedMenuIndex].BreakOut;
                    break;
                }
                else if (keyPressed == ConsoleKey.Enter) //Model object
                {
                    _currentModel = pageOfList[_selectedMenuIndex - _menuObjects.Count];
                    breakOut = _listObject?.BreakOut ?? false;
                    break;
                }
                else if (keyPressed == ConsoleKey.Escape)
                {
                    _listObject = new(_listObject?.List, null, _listObject?.BreakOut);
                    break;
                }
            }

            nextMethod = selectedMenu ?? _listObject?.Method ?? previousMenu;
            _listObject = new(null, _listObject?.Method, _listObject?.BreakOut);

            _selectedMenuIndex = 0;
            _menuObjects.Clear();

            if (!breakOut)
            {
                nextMethod();
            }
        }

        private void SetConfigValues()
        {
            try
            {
                string? listItemsEachPageValue = ConfigurationManager.AppSettings[
                    "listItemsEachPage"
                ];
                int itemsEachPage = int.Parse(listItemsEachPageValue ?? "1");

                if (itemsEachPage < 1)
                {
                    Logger.LogError("Illegal value for ItemsEachPage", new ArgumentNullException());
                }

                _itemsEachPage = itemsEachPage;
            }
            catch (Exception error)
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
                Console.WriteLine(
                    $"Page {_currentPage + 1} of {_numberOfPages} ({_itemsEachPage} pr. page)"
                );
            }
        }

        private List<Model> CreatePageOfList()
        {
            List<Model> pageOfList = new();
            int startPageIndex = _currentPage * _itemsEachPage;

            for (
                int i = startPageIndex;
                i < _listObject?.List?.Count && i < startPageIndex + _itemsEachPage;
                i++
            )
            {
                pageOfList.Add(_listObject?.List?[i]);
            }

            return pageOfList;
        }

        private int SetNumberOfPages()
        {
            return (int)Math.Ceiling((_listObject?.List?.Count ?? 0) / (double)_itemsEachPage);
        }

        private List<ConsoleKey> CreateListOfAllowedKeys(int pageOfListCount = 0)
        {
            List<ConsoleKey> allowedKeys = new();

            if (_selectedMenuIndex > 0)
            {
                allowedKeys.Add(ConsoleKey.UpArrow);
            }
            if (_selectedMenuIndex < (_menuObjects.Count + pageOfListCount) - 1)
            {
                allowedKeys.Add(ConsoleKey.DownArrow);
            }

            if (_listObject?.List?.Count > 0 && _currentPage > 0)
            {
                allowedKeys.Add(ConsoleKey.LeftArrow);
            }

            if (_listObject?.List?.Count > 0 && _currentPage < _numberOfPages - 1)
            {
                allowedKeys.Add(ConsoleKey.RightArrow);
            }

            return allowedKeys;
        }
    }
}
