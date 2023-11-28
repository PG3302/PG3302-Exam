namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers.Menus
{
    internal class NavigationMenus : UIController
    {
        // Main Menu
        protected void MainMenu()
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
    }
}
