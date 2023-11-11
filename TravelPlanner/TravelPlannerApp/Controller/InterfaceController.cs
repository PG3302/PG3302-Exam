using TravelPlanner.TravelPlannerApp.Controller.Menu;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Controller
{
    internal class InterfaceController
    {
        private User? _currentUser = null;
        private int _selectedMenuIndex = 0;
        private List<MenuObject> _menuObjects = new();
        private UserController userController = new();

        public void Start()
        {
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
            List<Capital> myList = new();

            for (int i = 0; i < 100; i++)
            {
                myList.Add(new Capital($"{i}", new Coordinate(0,0), Continent.Antarctica));
            }

            //GetUserSelectedList("List of capitals...", MainMenu, myList);
        }

        private void LoginMenu()
        {
            _menuObjects.Add(new("Wrong.", NotMe));

            GetUserSelectedMenu("This is the login menu...", MainMenu);
        }

        private void SearchMenu()
        {
            _menuObjects.Add(new("Wrong.", NotMe));

            GetUserSelectedMenu("This is the search menu...", MainMenu);
        }

        private void ExitConsole()
        {
            Console.WriteLine("Bye...");
        }

        //REMOVE
        private void NotMe()
        {
            Console.WriteLine("This is wrong");
        }

        private void PrintMenu(string title, List<Model>? list = null)
        {
            //int menuCount = _menuObjects.Count;

            //menucount = 4
            //listcount = 5

            Console.Clear();
            Console.WriteLine(title);

            //IF __selectedMenuIndex WORKS FOR LIST, MERGE FOR LOOPS

            //i = 0 - 3
            for (int i = 0; i < _menuObjects.Count; i++)
            {
                if (i == _selectedMenuIndex)
                    Console.Write("[O]");
                else
                    Console.Write("[ ]");

                Console.WriteLine($" {_menuObjects[i].Text}");
            }

            //i = 4 - 8
            for (int i = _menuObjects.Count; i < _menuObjects.Count + list?.Count; i++)
            {
                if (i == _selectedMenuIndex)
                    Console.Write("[O]");
                else
                    Console.Write("[ ]");

                Console.WriteLine($" {list?[i - _menuObjects.Count].ToString()}");
            }
        }

        private void GetUserSelectedMenu(string title, Action previousMenu)
        {
            List<ConsoleKey> allowedKeys = new();
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;

            Capital capital = new("Bob", new Coordinate(0, 0), Continent.NorthAmerica);


            while(selectedMenu == null)
            {
                PrintMenu(title, new List<Model> { capital, capital });

                allowedKeys.Clear();

                if (_selectedMenuIndex > 0)
                {
                    allowedKeys.Add(ConsoleKey.UpArrow);
                }

                if (_selectedMenuIndex < _menuObjects.Count - 1)
                {
                    allowedKeys.Add(ConsoleKey.DownArrow);
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
                else if (keyPressed == ConsoleKey.Enter)
                {
                    selectedMenu = _menuObjects[_selectedMenuIndex].Method;
                } 
                else if (keyPressed == ConsoleKey.Escape)
                {
                    break;
                }
            }

            nextMethod = selectedMenu ?? previousMenu;

            _menuObjects.Clear();

            nextMethod();
        }

        /*
        private void GetUserSelectedList(string title, Action previousMenu, List<Capital> list)
        {
            List<ConsoleKey> allowedKeys = new();
            ConsoleKey keyPressed;

            //Make wildcard
            Capital? selectedModel = null;

            //Action? selectedMenu = null;
            //Action nextMethod;

            while (selectedModel == null)
            {
                PrintMenu(title);

                allowedKeys.Clear();

                if (_selectedMenuIndex > 0)
                {
                    allowedKeys.Add(ConsoleKey.UpArrow);
                }

                if (_selectedMenuIndex < _menuObjects.Count - 1)
                {
                    allowedKeys.Add(ConsoleKey.DownArrow);
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
                else if (keyPressed == ConsoleKey.Enter)
                {
                    selectedMenu = _menuObjects[_selectedMenuIndex].Method;
                }
                else if (keyPressed == ConsoleKey.Escape)
                {
                    break;
                }
            }

            nextMethod = selectedMenu ?? previousMenu;

            _menuObjects.Clear();

            nextMethod();
        }
                */
    }
}