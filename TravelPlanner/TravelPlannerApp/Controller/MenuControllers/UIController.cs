using TravelDatabase.Data.DataType;
using TravelDatabase.Data.Log;
using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Service;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class UIController
    {
        private readonly MenuController _menuController = new();
        private readonly CapitalService _capitalService = new();
        private readonly TripService _tripService = new();
        private readonly UserService _userService = new();

        private UserModel? _currentUser = null;

        //!Only use _currentUser for user checks!
        private bool isLoggedIn = false;
        private bool isAdmin = false;

        public void Start()
        {
            MainMenu();
        }

        // Admin, user and login menu's
        private void AdminMenu() // Admin Menu's
        {
            _menuController.AddMenu("Edit user", AdminEditUser);
            _menuController.AddMenu("Edit trip", AdminEditTrip);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Welcome, Mr.Admin *brutally tips fedora*", MainMenu);
        }

        private void AdminEditUser()
        {
            _menuController.AddMenu("Back.", AdminMenu);
            _menuController.RunMenu("Edit user subpage, to be finished", MainMenu);
        }

        private void AdminEditTrip()
        {
            _menuController.AddMenu("Back.", AdminMenu);
            _menuController.RunMenu("Edit trip subpage, to be finished", MainMenu);
        }



        private void UserMenu() // Reg user Menu's
        {
            _menuController.AddMenu("Add trip/Search", TripAddMenu);
            _menuController.AddMenu("My upcomming trips", PlannedTripMenu);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Welcome, *User*", MainMenu); //Later add specified user-name
        }

        private void TripAddMenu()
        {
            _menuController.AddMenu("Back.", UserMenu);
            _menuController.RunMenu("Add trip/search, to be finished", MainMenu);
        }

        private void PlannedTripMenu()
        {
            _menuController.AddMenu("Back.", UserMenu);
            _menuController.RunMenu("My saved/upcomming trips, to be finished", MainMenu);
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

            try
            {
                UserModel addedUser = _userService.AddUser(name, email, isAdmin: false);

                _menuController.AddMenu("Back.", MainMenu);
                _menuController.RunMenu("User added to travelplanner DB: " + addedUser, MainMenu);
            }
            catch (Exception ex)
            {
                // Handle the exception and display an error message
                Console.WriteLine($"Failed to create user. Error: {ex.Message}");

                // You can also log the exception if needed
                Logger.LogError($"Failed to create user. Error: {ex}");

                // Add the "Back" menu to return to the main menu
                _menuController.AddMenu("Back.", MainMenu);

                // Run the menu with an error message at the top
                _menuController.RunMenu("Failed to create user. Please try again.", MainMenu);
            }
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








        private List<Model>? currentList = null;

        private void ListMenu()
        {
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.AddMenu("Filter", ListMenuFilter);
            _menuController.AddList(currentList ?? _capitalService.GetCapitalAll(), MainMenu);
            _menuController.RunMenu("List of travel locations.", MainMenu);
        }

        private void ListMenuFilter()
        {
            _menuController.AddMenu("Back.", ListMenu);
            _menuController.AddMenu("Continent", ListMenuFilterContinent);
            _menuController.RunMenu("Filter travel locations.", ListMenu);
        }

        private void ListMenuFilterContinent()
        {
            if (Enum.TryParse<Continent>(_menuController.GetCurrentChoice(), out Continent currentContinent))
            {
                currentList = _capitalService.GetCapitalByContinent(currentContinent);
                CapitalModel testc = _capitalService.GetCapitalByName("London");
                Logger.LogInfo("Bla " + testc);
                ListMenu();
            }
            else
            {
                _menuController.AddMenu("Back", ListMenuFilter);

                foreach (string continent in Enum.GetNames(typeof(Continent)))
                {
                    _menuController.AddMenu($"{continent}", ListMenuFilterContinent);
                }

                _menuController.RunMenu("Please select continent to filter.", MainMenu);
            }
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