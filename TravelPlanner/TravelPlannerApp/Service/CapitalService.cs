using TravelPlanner.TravelPlannerApp.Data.Objects;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service
{
    internal class CapitalService
    {
        private readonly MockCapitalDatabase capitalDatabase = new();

        public Capital? GetCapitalByName(string name)
        {
            Capital? requestedCapital = capitalDatabase.GetCapitalByName(name);

            return requestedCapital;
        }

        public Capital? GetCapitalById(long id)
        {
            return capitalDatabase.GetCapitalById(id);
        }
    }
}
