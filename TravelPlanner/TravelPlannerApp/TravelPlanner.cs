using TravelPlanner.TravelPlannerApp.Data.Objects;
using TravelPlanner.TravelPlannerApp.Service;

namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        public void Start()
        {
            Test();

            Console.WriteLine("I am the main program.");
        }

        public void Test()
        {
            //User newUser = new();

            UserService userService = new();

            Console.WriteLine(userService.GetUserByUsername("Ole"));
            Console.WriteLine(userService.GetUserByUsername("Dole"));
            Console.WriteLine(userService.GetUserByUsername("Doffen"));
            Console.WriteLine(userService.GetUserByUsername("Donald"));
        }
    }
}