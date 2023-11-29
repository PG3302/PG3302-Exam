using TravelPlanner.TravelPlannerApp.Controller.ConsoleControllers;
using TravelPlanner.TravelPlannerApp.Controller.MenuControllers;

namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        public void Start()
        {
            UIController interfaceController = new();
            interfaceController.Start();
        }
    }
}