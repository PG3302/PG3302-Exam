using TravelDatabase.Data.DataType;
using TravelDatabase.Data.Log;
using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Service;
using TravelPlanner.TravelPlannerApp.Controller.UserControllers;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class UIController
    {
        private readonly MenuController _menuController = new();
        private readonly CapitalService _capitalService = new();
        private readonly TripService _tripService = new();
        private readonly UserService _userService = new();
        private readonly UserController _userController = new();

        private UserModel? _currentUser = null;
        private List<Model>? _currentList = null;
        private string? _currentMessage;

        #region MAIN
        public void Start()
        {
            MainMenu();            
        }

        private void ExitConsole()
        {
            Console.Clear();
            Console.WriteLine("Hope you enjoyed your stay :)" +
                "\nPress any key to leave...");
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
                _menuController.AddMenu("Admin.", AdminMenu);
            }

            _menuController.AddMenu("Locations.", FilterCapitalList);
            _menuController.AddMenu("Exit.", ExitConsole);

            _menuController.RunMenu($"Welcome to Kristiania Travel Planner {_currentUser?.Name ?? ""} :)", ExitConsole);
        }
        #endregion

        #region ADMIN
        private void AdminMenu()
        {
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.AddMenu("Delete User.", DeleteUserMenu);
            _menuController.RunMenu("Welcome admin. Please select an option.", MainMenu);
        }

        private void DeleteUserMenu()
        {
            _menuController.AddMenu("Back.", AdminMenu);
            _menuController.AddList(_userService.GetUserAll(), DeleteUserSubMenu);
            _menuController.RunMenu("WARNING! Please select a user to delete permanently.", AdminMenu);
        }

        private void DeleteUserSubMenu()
        {
            UserModel? deletedUser = (UserModel?)_menuController.GetCurrentModel();

            _userService.DeleteUser(deletedUser?.Id ?? -1);

            MainMenu();
        }

        #endregion

        #region TRIP MENU
        private void TripMenu()
        {
            _menuController.AddMenu("Add Trip.", AddTripMenu);
            _menuController.AddMenu("List Trips.", SeeTripsMenu);
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.RunMenu("Please select what operation you would like for trips.", MainMenu);
        }

        private void AddTripMenu()
        {
            CapitalModel? departureCapital;
            CapitalModel? arrivalCapital;

            _currentMessage = "Please select departure capital...";
            FilterCapitalList();
            departureCapital = (CapitalModel?)_menuController.GetCurrentModel();

            _currentMessage = "Please select arrival capital...";
            FilterCapitalList();
            arrivalCapital = (CapitalModel?)_menuController.GetCurrentModel();

            if (departureCapital != null || arrivalCapital != null || _currentUser != null)
            {
                _tripService.AddTrip(_currentUser?.Email ?? "", departureCapital?.Id ?? -1, arrivalCapital?.Id ?? -1);
            } else
            {
                Logger.LogError("Wrong value when adding trips. ", new NullReferenceException());
            }

            MainMenu();
;       }

        private void SeeTripsMenu()
        {
            List<TripModel> tripList = new();

            _menuController.AddMenu("Back.", TripMenu);

            if (_currentUser?.IsAdmin ?? false)
            {
                _menuController.AddList(_tripService.GetTripAll(), TripMenu);
            } else
            {
                _menuController.AddList(_tripService.GetTripByUser(_currentUser?.Email ?? ""), TripMenu);
            }

            _menuController.RunMenu("List of all previous trips...", TripMenu);
        }
        #endregion

        #region LOGIN
        private void LoginMenu()
        {
            Console.Clear();

            Console.Write("Welcome :) Please log in, or leave blank to return to main menu." +
                "\nEmail: ");

            _currentMessage = _userController.GetUserString(true).ToLower();

            if (_currentMessage.Length > 0)
            {
                _currentUser = _userService.GetUserByEmail(_currentMessage);

                if (_currentUser == null)
                {
                    CreateUser();
                }

                _currentMessage = "";
            }

            MainMenu();
        }

        private void LogOutMenu()
        {
            _currentUser = null;
            MainMenu();
        }

        private void CreateUser()
        {
            string name;

            Console.Clear();

            Console.Write($"{_currentMessage} not found. Please create a new user..." +
                $"\nName: ");

            name = _userController.GetUserString();

            _currentUser = _userService.AddUser(name, _currentMessage ?? "");

            MainMenu();
        }
        #endregion

        #region LIST MENUS
        private void FilterCapitalList()
        {
            #region MAIN LIST PART
            _menuController.AddMenu("Back.", MainMenu);

            //Added due to bug and too little time to fix.
            if (_menuController.GetCurrentChoice() != "Add Trip.")
            {
                _menuController.AddMenu("Filter", FilterCapitalList, true);
            }

            if ((_menuController.GetCurrentChoice() == "Locations.") || (Enum.TryParse<Continent>(_menuController.GetCurrentChoice(), out Continent currentContinent)))
            {
                _menuController.AddList(_currentList ?? _capitalService.GetCapitalAll(), MainMenu);
            } else 
            {
                _menuController.AddList(_currentList ?? _capitalService.GetCapitalAll(), MainMenu, true);
            }
            
            _menuController.RunMenu(_currentMessage ?? $"List of capitals.", MainMenu);

            if (_menuController.GetCurrentModel() != null)
            {
                return;
            }
            #endregion

            #region SELECT FILTER PART
            _menuController.AddMenu("Back.", FilterCapitalList);
            _menuController.AddMenu("Continent", FilterCapitalList, true);

            _menuController.RunMenu($"Filter capital.", FilterCapitalList);
            #endregion

            #region FILTER PART
            _menuController.AddMenu("Back", FilterCapitalList);

            //Continent currentContinent;

            foreach (string continent in Enum.GetNames(typeof(Continent)))
            {
                _menuController.AddMenu($"{continent}", FilterCapitalList, true);
            }

            _menuController.RunMenu("Please select continent to filter.", MainMenu);

            if (Enum.TryParse<Continent>(_menuController.GetCurrentChoice(), out currentContinent))
            {
                _currentList = _capitalService.GetCapitalByContinent(currentContinent);
                FilterCapitalList();
            }

            #endregion
        }
        #endregion
    }
}
