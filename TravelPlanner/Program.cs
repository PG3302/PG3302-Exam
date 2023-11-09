using TravelPlanner.TravelPlannerApp;

namespace TravelPlanner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TravelPlannerApp.TravelPlanner travelPlanner = new();

            //TravelPlannerProvider travelPlannerProvider = new(new TravelPlannerProvider("Resources\worldSqlite.db"); - - For å gi hvilken con string vi ønsker å bruke. Setter en default value for nå.

            travelPlanner.Start();
        }
    }
}