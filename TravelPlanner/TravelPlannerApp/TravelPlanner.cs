using TravelPlanner.TravelPlannerApp.Controller.MenuControllers;

namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        public void Start()
        {
            //Console.WriteLine("I am the main program.");

            InterfaceController interfaceController = new();
            interfaceController.Start();
        }
    }
}