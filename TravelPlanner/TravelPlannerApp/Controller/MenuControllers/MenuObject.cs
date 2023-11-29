namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class MenuObject(string text, Action method, bool breakOut = false)
    {
        internal Action Method { get; private set; } = method;
        internal string Text { get; private set; } = text;
        internal bool BreakOut { get; private set; } = breakOut;
    }
}
