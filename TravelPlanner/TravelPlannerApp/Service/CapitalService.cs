using TravelDatabase.Data.DataType;
using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service
{
    internal class CapitalService
    {
        private readonly CapitalRepository _capitalRepository = new();

        internal List<Model> GetCapitalAll()
        {
            List<Model> returnList = [.. _capitalRepository.GetCapitalAll()];

            return returnList;
        }

        internal List<Model> GetCapitalByContinent(Continent continent)
        {
            List<Model> returnList = [.. _capitalRepository.GetCapitalByContinent(continent)];

            return returnList;
        }
    }
}
