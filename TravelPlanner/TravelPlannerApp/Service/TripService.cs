using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service {
	internal class TripService
    {
        private readonly TripRepository _tripRepository = new();
        private readonly UserRepository _userRepository = new();
        private readonly CapitalRepository _capitalRepository = new();

        public TripModel AddTrip(string userEmail, int departureCapitalId, int arrivalCapitalId)
        {
            UserModel user = _userRepository.GetUserByUsername(userEmail);
            CapitalModel departureCapital = _capitalRepository.GetCapitalById(departureCapitalId);
            CapitalModel arrivalCapital = _capitalRepository.GetCapitalById(arrivalCapitalId);
            TripModel newTrip = new(user, departureCapital, arrivalCapital);

            return _tripRepository.AddTrip(newTrip);
        }

        public List<TripModel> GetTripAll()
        {
            return _tripRepository.GetTripAll();
        }

        public TripModel GetTripById(int tripId)
        {
            return _tripRepository.GetTripById(tripId);
        }

        public List<TripModel> GetTripByUser(string userEmail)
        {
            return _tripRepository.GetTripByUser(userEmail);
        }

        public List<TripModel> GetTripByCapital(int capitalId)
        {
            return _tripRepository.GetTripsByCapital(capitalId);
        }
    }
}
