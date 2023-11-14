using TravelPlanner.TravelPlannerApp.Controller.UserControllers;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Repository.Database;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class MenuController
    {
        private User? _currentUser = null;
        private Model? _currentModel = null;
        private int _selectedMenuIndex = 0;
        private List<MenuObject> _menuObjects = new();
        private UserController userController = new();
        private bool isLoggedIn = false;
        private bool isAdmin = false;


        //REMOVE BELOW
        List<Model>? MockCapitalList;
        private void TmpCreateMockList()
        {
            MockCapitalList = new();

            for (int i = 0; i < 95; i++)
            {
                MockCapitalList.Add(new Capital($"{i}", new Coordinate(RandomNumber(), RandomNumber()), Continent.Antarctica));
            }
        }
        private void TmpLoginSomething()
        {
            Console.Clear();
            _menuObjects.Add(new ("Login", LoginSubPage));
            _menuObjects.Add(new("Create user", CreateUser));
            _menuObjects.Add(new("Back.", MainMenu));
            GetUserSelectedMenu("You are currently not logged in. Do you wish you log in or create a user?", MainMenu);
        }

        private void LoginSubPage()
        {
            Console.Clear();
            _menuObjects.Add(new("Standard User", LoginRegUser));
            _menuObjects.Add(new("Admin user", LoginAdminUser));
            _menuObjects.Add(new("Back.", MainMenu));
            GetUserSelectedMenu("Login/login", MainMenu);
        }
        
        private void LoginRegUser()
        {
            isLoggedIn = true;
            MainMenu();
        }

        private void LoginAdminUser()
        {
            isLoggedIn = true;
            isAdmin = true;
            MainMenu();
        }
        private void LogOutMenu()
        {
            isLoggedIn = false;
            isAdmin = false;
            MainMenu();
        }

        private void CreateUser()
        {
            Console.Clear();            
            Console.WriteLine("Test");
            _menuObjects.Add(new("Back.", MainMenu));
            GetUserSelectedMenu("Create user", MainMenu);
        }
        
        private void AdminMenu()
        {
            _menuObjects.Add(new("Do admin thing nr 1", MainMenu));
            _menuObjects.Add(new("Do admin thing nr 2", MainMenu));
            _menuObjects.Add(new("Logout", MainMenu));
            GetUserSelectedMenu("Welcome, Mr.Admin *brutally tips fedora*", MainMenu);
        }

        private void UserMenu()
        {  
            _menuObjects.Add(new("My Trips", MainMenu));
            _menuObjects.Add(new("search?", MainMenu));
            _menuObjects.Add(new("Logout", MainMenu));
            GetUserSelectedMenu("Welcome, *User*", MainMenu);// Later add specified user-name
        }

        private void LoginMenu()
        {
            if(isLoggedIn == false)
            {
               TmpLoginSomething();
            }
            else
            {
                if(isAdmin)
                {         
                    AdminMenu();                       
                    _menuObjects.Add(new("Back.", MainMenu));
                    GetUserSelectedMenu("logget inn som admin", MainMenu);

                }
                else
                {
                    UserMenu();
                    _menuObjects.Add(new("Back.", MainMenu));
                    GetUserSelectedMenu("logget inn med vanlig acc", UserMenu);
                }
            }
            _menuObjects.Add(new("Back.", MainMenu));
        }


        
        private int RandomNumber()
        {
            Random rng = new();
            return rng.Next(-1000, 1000);
        }

        private void PrintCapital()
        {
            Console.Clear();
            Console.WriteLine($"Your home capital is: {_currentModel}... Good choice. Press enter to continue...");
            Console.ReadKey();
            MainMenu();
        }
        //REMOVE ABOVE


        public void Start()
        {
            //Remove
            TmpCreateMockList();

            MainMenu();
        }

        private void MainMenu()
        {
            if(isLoggedIn == false)
            {
                _menuObjects.Add(new("Login.", LoginMenu));
            }
            else
            {
                _menuObjects.Add(new("Logout.", LogOutMenu));
            }
            
            _menuObjects.Add(new("List.", ListMenu));
            if(isLoggedIn == true)
            {
                _menuObjects.Add(new("My Trips", LogOutMenu));
                if(isAdmin)
                {
                    _menuObjects.Add(new("Admin Page", LogOutMenu));
                }
            }

            _menuObjects.Add(new("Exit.", ExitConsole));
            if(isLoggedIn == true)
            {
                if(isAdmin)
                {
                    GetUserSelectedMenu("Welcome to Kristiania Travel Planner, Admin", ExitConsole);
                }
                else{
                    GetUserSelectedMenu("Welcome to Kristiania Travel Planner, *User Name*", ExitConsole);
                }
            }
            else
            {
                GetUserSelectedMenu("Welcome to Kristiania Travel Planner...", ExitConsole);
            }

        }


        private void ListMenu()
        {
            _menuObjects.Add(new("Back.", MainMenu));

            GetUserSelectedMenu("Please select your home capital...", MainMenu, MockCapitalList, PrintCapital);
        }

        private void ExitConsole()
        {
            Console.Clear();
            Console.WriteLine("Quit stuff complete... Press any key to leave...");
            Console.ReadKey();
            Environment.Exit(0);
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

        private void GetUserSelectedMenu(string title, Action previousMenu, List<Model>? list = null, Action? selectedModelMenu = null)
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