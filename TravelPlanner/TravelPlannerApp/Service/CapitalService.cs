using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service {
	internal class CapitalService
    {
        private readonly MockCapitalDatabase capitalDatabase = new();

        public CapitalModel? GetCapitalByName(string name)
        {
            CapitalModel? requestedCapital = capitalDatabase.GetCapitalByName(name);

            return requestedCapital;
        }

        public CapitalModel? GetCapitalById(long id)
        {
            return capitalDatabase.GetCapitalById(id);
        }
    }
}
