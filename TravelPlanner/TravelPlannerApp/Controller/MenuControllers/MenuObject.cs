namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class MenuObject(string text, Action method, bool breakOut = false)
    {
        public Action Method { get; private set; } = method;
        public string Text { get; private set; } = text;
        public bool BreakOut { get; private set; } = breakOut;
    }
}
