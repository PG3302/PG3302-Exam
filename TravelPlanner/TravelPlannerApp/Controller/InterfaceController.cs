using TravelPlanner.TravelPlannerApp.Data.Model;

namespace TravelPlanner.TravelPlannerApp.Controller
{
    internal class InterfaceController
    {
        private User? _currentUser = null;

        public void Start()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            //bool currentUserAdmin = CurrentUser.IsAdmin;
            //int menuChoice;
            //Console.Clear();

            int menuIndex = 1;

            Console.Write($"Welcome to Kristiania Travel Planner..." +
                "\n1. Login." +
                "\n2. Create travel plan." +
                "\n3. See previous travels." +
                "\n4. Log out." +
                $"{(currentUserAdmin ? "\n5. Admin stuff." : "")}" +
                "\nSelected Menu: ");

            menuChoice = currentUserAdmin ? UserController.GetUserMenuChoice(1, 5) : UserController.GetUserMenuChoice(1, 4);

            if (menuChoice == 1)
            {
                Console.WriteLine("Find location coming later...");
                MainMenu();
            }
            else if (menuChoice == 2)
            {
                TravelPlanLocation();
            }
            else if (menuChoice == 3)
            {
                Console.WriteLine("Previous travels coming later...");
                MainMenu();
            }
            else if (menuChoice == 4)
            {
                CurrentUser = null;
                Console.Clear();
                Start();
            }
            else if (menuChoice == 5)
            {
                AdminMenu();
            }
        }
    }
}
