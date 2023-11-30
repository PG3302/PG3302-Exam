namespace TravelPlanner.TravelPlannerApp.Controller.ConsoleControllers
{
    internal class ConsoleController
    {
        internal ConsoleController()
        {
            Console.Title = "Kristiania Travel Planner";
            HideCursor();
        }

        internal void ShowCursor()
        {
            Console.CursorVisible = true;
        }

        internal void HideCursor()
        {
            Console.CursorVisible = false;
        }

        internal void MoveCursor(int left, int top)
        {
            Console.SetCursorPosition(left, Console.CursorTop - top);
        }
    }
}
