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

        private UserModel? _currentUser = new(1, "TestUser", "testUser@Test.com");

        //REMOVE
        bool isLoggedIn = false;
        bool isAdmin = false;

        #region MAIN
        public void Start()
        {
            MainMenu();
        }

        private void ExitConsole()
        {
            Console.Clear();
            Console.WriteLine("Hope you enjoyed your stay :)\nPress any key to leave...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void MainMenu()
        {
            _currentList = null;
            _menuController.ResetMenuController();

            if (_currentUser != null)
            {
                _menuController.AddMenu("Logout.", LogOutMenu);
                _menuController.AddMenu("Trips.", TripMenu);
            } else
            {
                _menuController.AddMenu("Login.", LoginMenu);
            }

            if (_currentUser?.IsAdmin ?? false)
            {
                _menuController.AddMenu("Admin", AdminMenu);
            }

            _menuController.AddMenu("tmp", TripListMenu);

            _menuController.AddMenu("Locations.", CapitalListMenu);
            _menuController.AddMenu("Exit.", ExitConsole);

            _menuController.RunMenu($"Welcome to Kristiania Travel Planner {_currentUser?.Name ?? ""} :)", ExitConsole);
        }
        #endregion

        #region ADMIN

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

        #endregion

        #region TRIP MENU
        private void TripMenu()
        {
            _menuController.AddMenu("Add Trip", AddTripMenu);
            _menuController.AddMenu("List Trips.", SeeTripsMenu);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Please select what operation you would like for trips.", MainMenu);
        }

        private void AddTripMenu()
        {
            CapitalModel? departureCapital;
            CapitalModel? arrivalCapital;

            _currentMessage = "Please select departure capital...";
            CapitalListMenu();
            departureCapital = (CapitalModel?)_menuController.GetCurrentModel();

            _currentMessage = "Please select arrival capital...";
            CapitalListMenu();
            arrivalCapital = (CapitalModel?)_menuController.GetCurrentModel();

            if (departureCapital != null || arrivalCapital != null || _currentUser != null)
            {
                _tripService.AddTrip(_currentUser?.Email ?? "", departureCapital?.Id ?? -1, arrivalCapital?.Id ?? -1);
            } else
            {
                Logger.LogError("Wrong value when adding trips. ", new NullReferenceException());
            }
;       }

        private void SeeTripsMenu()
        {

        }
        #endregion

        #region LOGIN


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
                    _menuController.RunMenu("logget inn med vanlig acc", MainMenu);
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
            _menuController.RunMenu(
                "You are currently not logged in. Do you wish you log in or create a user?",
                MainMenu
            );
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
            Console.Write("Enter the name: ");
            string name = Console.ReadLine();

            Console.Write("Enter the email: ");
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

        #endregion

        #region LIST MENUS
        private void CapitalListMenu()
        {
            _currentModelType = ModelType.Capital;
            ListMenu();
        }

        private void TripListMenu()
        {
            _currentModelType = ModelType.Trip;
            ListMenu();
        }

        private List<Model>? _currentList = null;
        private ModelType? _currentModelType = null;
        private string? _currentMessage;

        private void ListMenu()
        {
            #region MAIN LIST PART
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.AddMenu("Filter", ListMenu, true);

            if (_currentModelType == ModelType.Capital)
            {
                if ((_menuController.GetCurrentChoice() == "Locations.") || (Enum.TryParse<Continent>(_menuController.GetCurrentChoice(), out Continent currentContinent)))
                {
                    _menuController.AddList(_currentList ?? _capitalService.GetCapitalAll(), MainMenu);
                } else 
                {
                    _menuController.AddList(_currentList ?? _capitalService.GetCapitalAll(), MainMenu, true);
                }
            }
            else if (_currentModelType == ModelType.Trip)
            {
                _menuController.AddList(_currentList ?? _tripService.GetTripAll(), MainMenu);
            }
            else if (_currentModelType == ModelType.User)
            {
                _menuController.AddList(_currentList ?? _userService.GetUserAll(), MainMenu);
            }
            else
            {
                Logger.LogError(
                    "No currentModeType found in list menu.",
                    new MissingFieldException()
                );
            }

            _menuController.RunMenu(_currentMessage ?? $"List of {_currentModelType}s.", MainMenu);

            if (_menuController.GetCurrentModel() != null)
            {
                return;
            }
            #endregion

            #region SELECT FILTER PART
            _menuController.AddMenu("Back.", ListMenu);
            if (_currentModelType == ModelType.Capital)
            {
                _menuController.AddMenu("Continent", ListMenu, true);
            }
            else if (_currentModelType == ModelType.Trip) { }
            else if (_currentModelType == ModelType.User) { }

            _menuController.RunMenu($"Filter {_currentModelType}.", ListMenu);
            #endregion

            #region FILTER PART
            _menuController.AddMenu("Back", ListMenu);

            if (_currentModelType == ModelType.Capital)
            {
                foreach (string continent in Enum.GetNames(typeof(Continent)))
                {
                    _menuController.AddMenu($"{continent}", ListMenu, true);
                }

                _menuController.RunMenu("Please select continent to filter.", MainMenu);

                if (
                    Enum.TryParse<Continent>(
                        _menuController.GetCurrentChoice(),
                        out Continent currentContinent
                    )
                )
                {
                    _currentList = _capitalService.GetCapitalByContinent(currentContinent);
                    ListMenu();
                }
            }
            #endregion
        }
        #endregion
    }
}
