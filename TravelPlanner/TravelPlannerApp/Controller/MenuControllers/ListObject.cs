using TravelDatabase.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class ListObject(List<Model>? list, Action? method, bool? breakOut)
    {
        public Action? Method { get; private set; } = method;
        public List<Model>? List { get; private set; } = list;
        public bool? BreakOut { get; set; } = breakOut;
    }
}
