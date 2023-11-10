using System.Data;
using System;
using TravelPlanner.TravelPlannerApp.Controller.Menu;
using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Model;

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
            _menuObjects.Add(new("Search.", SearchMenu));
            _menuObjects.Add(new("Exit.", ExitConsole));

           GetUserSelectedMenu("Welcome to Kristiania Travel Planner...", ExitConsole);
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

        private void PrintMenu(string title)
        {
            Console.Clear();
            Console.WriteLine(title);

            for (int i = 0; i < _menuObjects.Count; i++)
            {
                if (i == _selectedMenuIndex)
                    Console.Write("[O]");
                else
                    Console.Write("[ ]");

                Console.WriteLine($" {_menuObjects[i].Text}");
            }
        }

        private void GetUserSelectedMenu(string title, Action previousMenu)
        {
            List<ConsoleKey> allowedKeys = new();
            ConsoleKey keyPressed;
            Action? selectedMenu = null;
            Action nextMethod;

            while(selectedMenu == null)
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
    }
}