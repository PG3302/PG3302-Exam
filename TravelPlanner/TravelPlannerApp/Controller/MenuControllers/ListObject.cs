using TravelDatabase.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class ListObject
    {
        public Action? Method { get; private set; }
        public List<Model>? List { get; private set; }

        public ListObject(List<Model>? list, Action? method)
        {
            Method = method;
            List = list;
        }
    }
}
