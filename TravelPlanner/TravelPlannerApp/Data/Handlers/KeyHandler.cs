namespace TravelPlanner.TravelPlannerApp.Data.Handlers
{
    internal static class KeyHandler
    {
        public static void Test()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.UpArrow)
            {

            }

            Console.WriteLine(Console.ReadKey(true).KeyChar.ToString());
            Console.ReadLine();
        }
    }
}