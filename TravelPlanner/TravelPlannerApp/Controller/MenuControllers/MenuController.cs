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

        public void AddMenu(string menuText, Action menuMethod)
        {
            MenuObject menuObject = new(menuText, menuMethod);

            _menuObjects.Add(menuObject);
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
        }

        public void RunMenu(string title, Action previousMenu, List<Model>? list = null, Action? selectedModelMenu = null)
        {
            int currentPage = 0;
            List<ConsoleKey> allowedKeys = new();
            List<Model>? pageOfList = CreatePageOfList(list, currentPage);
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;

            while (selectedMenu == null)
            {
                PrintMenu(title, pageOfList);

                allowedKeys = CreateListOfAllowedKeys(currentPage, pageOfList.Count, list);
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
                    pageOfList = CreatePageOfList(list, currentPage);
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    currentPage++;
                    _selectedMenuIndex = 0;
                    pageOfList = CreatePageOfList(list, currentPage);
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
                    selectedModelMenu = null;
                    break;
                }
            }

            nextMethod = selectedMenu ?? selectedModelMenu ?? previousMenu;

            _selectedMenuIndex = 0;
            _menuObjects.Clear();

            nextMethod();
        }

        private List<Model> CreatePageOfList(List<Model>? list, int page, int itemsEachPage = 10)
        {
            List<Model> pageOfList = new();
            int startPageIndex = page * itemsEachPage;

            for (int i = startPageIndex; i < list?.Count && i < startPageIndex + itemsEachPage; i++)
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