using TravelDatabase.Data.DataType;
using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service {
	internal class CapitalService
    {
        private readonly CapitalRepository _capitalRepository = new();
        
        public List<Model> GetCapitalAll()
        {
            List<Model> returnList = [.. _capitalRepository.GetCapitalAll()];

            return returnList;
        }
        
        public CapitalModel GetCapitalById(int capitalId)
        {
            return _capitalRepository.GetCapitalById(capitalId);
        }

        public CapitalModel GetCapitalByName(string name)
        {
            return _capitalRepository.GetCapitalByName(name);
        }

        public List<CapitalModel> GetCapitalByContinent(Continent continent)
        {
            return _capitalRepository.GetCapitalByContinent(continent);
        }
    }
}
