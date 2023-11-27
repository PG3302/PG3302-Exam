using TravelDatabase.Repositories;

namespace TravelPlanner
{
    internal class Program
    {
        static void Main(string[] args)
        {
			InitDatabase.InitFromCsv();

			TravelPlannerApp.TravelPlanner travelPlanner = new();

            travelPlanner.Start();
        }
    }
}