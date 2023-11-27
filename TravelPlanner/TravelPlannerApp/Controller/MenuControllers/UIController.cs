﻿namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class UIController
    {
        private MenuController _menuController = new();
        private User? _currentUser = null;

        //!Only use _currentUser for user checks!
        private bool isLoggedIn = false;
        private bool isAdmin = false;

        public void Start()
        {
            MainMenu();
        }

        // Admin, user and login menu's
        private void AdminMenu()
        {
            _menuController.AddMenu("Edit user", MainMenu);
            _menuController.AddMenu("Edit trip", MainMenu);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Welcome, Mr.Admin *brutally tips fedora*", MainMenu);
        }

        private void UserMenu()
        {
            _menuController.AddMenu("Add trip/Search", MainMenu);
            _menuController.AddMenu("My upcomming trips", MainMenu);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Welcome, *User*", MainMenu); //Later add specified user-name
        }

        private void LoginMenu()
        {
            if (isLoggedIn == false)
            {
                TmpLoginSomething();
            }
            else
            {
                if (isAdmin)
                {
                    _menuController.AddMenu("Back.", MainMenu);
                    _menuController.RunMenu("logget inn som admin", AdminMenu);

                }
                else
                {
                    _menuController.AddMenu("Back.", MainMenu);
                    _menuController.RunMenu("logget inn med vanlig acc", UserMenu);
                }
            }
            _menuController.AddMenu("Back.", MainMenu);
        }

        // Login/Logout section
        private void TmpLoginSomething()
        {
            Console.Clear();
            _menuController.AddMenu("Login", LoginSubPage);
            _menuController.AddMenu("Create user", CreateUser);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("You are currently not logged in. Do you wish you log in or create a user?", MainMenu);
        }

        private void LoginSubPage()
        {
            Console.Clear();
            _menuController.AddMenu("Standard User", LoginRegUser);
            _menuController.AddMenu("Admin user", LoginAdminUser);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Login/login", MainMenu);
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


        // Create user
        private void CreateUser()
        {
            Console.Clear();
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Create user", MainMenu);
        }



        // Main Menu
        private void MainMenu()
        {
            if (isLoggedIn == false)
            {
                _menuController.AddMenu("Login.", LoginMenu);
            }
            else
            {
                _menuController.AddMenu("Logout.", LogOutMenu);
            }

            _menuController.AddMenu("List.", ListMenu);

            if (isLoggedIn == true)
            {
                if (!isAdmin)
                {
                    _menuController.AddMenu("My Trips", UserMenu);
                }

                if (isAdmin)
                {
                    _menuController.AddMenu("Admin Page", AdminMenu);
                }
            }

            _menuController.AddMenu("Exit.", ExitConsole);

            if (isLoggedIn == true)
            {
                if (isAdmin)
                {
                    _menuController.RunMenu("Welcome to Kristiania Travel Planner, Admin", ExitConsole);
                }
                else
                {
                    _menuController.RunMenu("Welcome to Kristiania Travel Planner, *User Name*", ExitConsole);
                }
            }
            else
            {
                _menuController.RunMenu("Welcome to Kristiania Travel Planner...", ExitConsole);
            }

        }


        private void ListMenu()
        {
            _menuController.AddMenu("Back.", MainMenu);

            _menuController.RunMenu("Please select your home capital...", MainMenu, MockCapitalList, PrintCapital);
        }

        private void ExitConsole()
        {
            Console.Clear();
            Console.WriteLine("Quit stuff complete... Press any key to leave...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}