using TravelDatabase.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class ListObject(List<Model>? list, Action? method, bool? breakOut)
    {
        internal Action? Method { get; private set; } = method;
        internal List<Model>? List { get; private set; } = list;
        internal bool? BreakOut { get; set; } = breakOut;
    }
}
