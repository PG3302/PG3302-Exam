using TravelPlanner.TravelPlannerApp.Controller.UserControllers;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class InterfaceController
    {
        private User? _currentUser = null;
        private Model? _currentModel = null;
        private int _selectedMenuIndex = 0;
        private List<MenuObject> _menuObjects = new();
        private UserController userController = new();


        //REMOVE BELOW
        List<Model>? _tmpList;
        private void TmpCreateMockList()
        {
            _tmpList = new();

            for (int i = 0; i < 95; i++)
            {
                _tmpList.Add(new Capital($"{i}", new Coordinate(0, 0), Continent.Antarctica));
            }
        }
        //REMOVE ABOVE


        public void Start()
        {
            TmpCreateMockList();

            MainMenu();
        }

        private void MainMenu()
        {
            _menuObjects.Add(new("Login.", LoginMenu));
            _menuObjects.Add(new("List.", ListMenu));
            _menuObjects.Add(new("Exit.", ExitConsole));

            GetUserSelectedMenu("Welcome to Kristiania Travel Planner...", ExitConsole);
        }

        private void ListMenu()
        {
            _menuObjects.Add(new("Wrong.", NotMe));

            GetUserSelectedMenu("List of capitals...", MainMenu, _tmpList);
        }

        private void LoginMenu()
        {
            _menuObjects.Add(new("Wrong.", NotMe));

            GetUserSelectedMenu("This is the login menu...", MainMenu);
        }

        private void ExitConsole()
        {
            Console.Clear();
            Console.WriteLine("Quit stuff complete... Bye...");
            Console.ReadKey();
        }

        //REMOVE
        private void NotMe()
        {
            Console.Clear();
            Console.WriteLine("This is wrong");
            Console.ReadKey();
        }

        private void PrintMenu(string title, List<Model>? list = null)
        {
            Console.Clear();
            Console.WriteLine(title);

            for (int i = 0; i < _menuObjects.Count + list?.Count; i++)
            {
                if (i == _selectedMenuIndex)
                {
                    Console.Write("[O]");
                }
                else
                {
                    Console.Write("[ ]");
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

        private void GetUserSelectedMenu(string title, Action previousMenu, List<Model>? list = null)
        {
            int numberOfPages = (int)Math.Ceiling((list?.Count ?? 0) / 10.0);
            int currentPage = 0;
            List<ConsoleKey> allowedKeys = new();
            List<Model>? pageOfList = CreatePageOfList(list, currentPage);
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;

            while (selectedMenu == null)
            {
                PrintMenu(title, pageOfList);

                allowedKeys.Clear();

                if (_selectedMenuIndex > 0)
                {
                    allowedKeys.Add(ConsoleKey.UpArrow);
                }
                if (_selectedMenuIndex < (_menuObjects.Count + pageOfList.Count) - 1)
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

                keyPressed = userController.GetUserMenuChoiceKey(allowedKeys);

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
                else if (keyPressed == ConsoleKey.Enter)
                {
                    if (_selectedMenuIndex < _menuObjects.Count)
                    {
                        selectedMenu = _menuObjects[_selectedMenuIndex].Method;
                    }
                    else
                    {
                        _currentModel = pageOfList[_selectedMenuIndex - _menuObjects.Count];
                        Console.WriteLine($"You selected: {_currentModel}");
                        Console.ReadKey();
                        break;
                    }
                }
                else if (keyPressed == ConsoleKey.Escape)
                {
                    break;
                }
            }

            nextMethod = selectedMenu ?? previousMenu;

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
    }
}