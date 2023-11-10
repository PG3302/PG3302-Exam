using TravelPlanner.TravelPlannerApp.Controller;

namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        public void Start()
        {
            UserController userController = new();

            Console.WriteLine("I am the main program.");

            userController.GetUserMenuChoiceKey(new List<ConsoleKey> { ConsoleKey.UpArrow, ConsoleKey.DownArrow });
        }
    }
}