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
            _menuObjects.Add(new("Login."/*, MethodName here */));
            _menuObjects.Add(new("Search."/*, MethodName here */));
            _menuObjects.Add(new("Exit."/*, MethodName here */));

           GetUserSelectedMenu("Welcome to Kristiania Travel Planner...");
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

        private ConsoleKey? GetUserSelectedMenu(string title)
        {
            bool menuSelected = false;
            List<ConsoleKey> allowedKeys = new();
            ConsoleKey keyPressed;

            while(!menuSelected)
            {
                PrintMenu(title);

                allowedKeys.Clear();

                if (_selectedMenuIndex > 0)
                    allowedKeys.Add(ConsoleKey.UpArrow);
                if (_selectedMenuIndex < _menuObjects.Count - 1)
                    allowedKeys.Add(ConsoleKey.DownArrow);

                keyPressed = userController.GetUserMenuChoiceKey(allowedKeys);

                if (keyPressed == ConsoleKey.Enter || keyPressed == ConsoleKey.Escape)
                {
                    _menuObjects.Clear();
                    return keyPressed;
                }
                else if (keyPressed == ConsoleKey.UpArrow)
                {
                    _selectedMenuIndex--;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    _selectedMenuIndex++;
                }
            }

            Logger.LogError("Interface controller error when getting keys.", new ArgumentNullException());
            return null;
        }
    }
}