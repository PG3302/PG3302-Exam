namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class MenuObject
    {
        public Action Method { get; private set; }
        public string Text { get; private set; }

        public MenuObject(string text, Action method)
        {
            Method = method;
            Text = text;
        }
    }
}
