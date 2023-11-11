namespace TravelPlanner.TravelPlannerApp.Controller.ConsoleControllers
{
    internal static class ConsoleController
    {
        public static void SetConsoleDisplay()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Title = "Kristiania Travel Planner";
        }
    }
}
