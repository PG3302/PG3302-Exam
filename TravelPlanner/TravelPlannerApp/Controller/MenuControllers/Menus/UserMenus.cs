using TravelDatabase.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers.Menus
{
    internal class UserMenus : UIController
    {
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
            Console.WriteLine("Enter the name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the email: ");
            string email = Console.ReadLine();

            UserModel addedUser = _userService.AddUser(name, email, isAdmin: true);

            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("User added to travelplanner DB: " + addedUser, MainMenu);
        }



        private void ListMenu(List<Model>? list = null)
        {
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.AddMenu("Filter.", ListMenuFilterCapital);

            _menuController.AddList(list ?? _capitalService.GetCapitalAll(), MainMenu);

            _menuController.RunMenu("List of travel locations...", MainMenu);
        }

        private void ListMenuFilterCapital()
        {
            //_menuController.AddMenu()
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
