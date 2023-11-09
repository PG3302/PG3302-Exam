using TravelPlanner.TravelPlannerApp.DataProviders;

namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        private readonly TravelPlannerProvider _travelDataProvider;
        public void Start()
        {
            Console.WriteLine("I am the main program.");

            TravelPlannerSqlite tp = new TravelPlannerSqlite("C:\\Users\\carla\\Source\\Repos\\PG3302-Exam-\\TravelPlanner\\TravelPlannerApp\\Resources\\worldSqlite.db");

           tp.getAnything();
            
        }
    }
}
