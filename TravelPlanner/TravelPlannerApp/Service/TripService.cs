using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service
{
    internal class TripService
    {
        private readonly TripRepository _tripRepository = new();
        private readonly UserRepository _userRepository = new();
        private readonly CapitalRepository _capitalRepository = new();

        internal TripModel AddTrip(string userEmail, int departureCapitalId, int arrivalCapitalId)
        {
            UserModel? user = _userRepository.GetUserByEmail(userEmail);
            CapitalModel? departureCapital = _capitalRepository.GetCapitalById(departureCapitalId);
            CapitalModel? arrivalCapital = _capitalRepository.GetCapitalById(arrivalCapitalId);
            TripModel newTrip = new(null, user, departureCapital, arrivalCapital);

            return _tripRepository.AddTrip(newTrip);
        }

        internal List<Model> GetTripAll()
        {
            List<Model> returnList = [.. _tripRepository.GetTripAll()];

            return returnList;
        }

        public List<Model> GetTripByUser(string userEmail)
        {
            List<Model> returnList = [.. _tripRepository.GetTripByUser(userEmail)];

            return returnList;
        }
    }
}
