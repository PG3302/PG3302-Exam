using TravelDatabase.Repositories;

namespace TravelPlanner
{
    internal class Program
    {
        static void Main(string[] args)
        {
			InitDatabase.Init();

			TravelPlannerApp.TravelPlanner travelPlanner = new();

            travelPlanner.Start();
        }
    }
}