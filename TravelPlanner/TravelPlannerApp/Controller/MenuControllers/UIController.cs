using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Service;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class UIController
    {
        protected readonly MenuController _menuController = new();
        protected readonly CapitalService _capitalService = new();
        protected readonly TripService _tripService = new();
        protected readonly UserService _userService = new();

        protected UserModel? _currentUser = null;

        //!Only use _currentUser for user checks!
        protected bool isLoggedIn = false;
        protected bool isAdmin = false;

        public void Start()
        {
            MainMenu();
        }

        private void ExitConsole()
        {
            Console.Clear();
            Console.WriteLine("Hope you had a nice stay :) Press any key to leave...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
