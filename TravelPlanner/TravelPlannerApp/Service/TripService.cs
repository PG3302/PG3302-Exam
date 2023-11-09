using TravelPlanner.TravelPlannerApp.Data.Objects;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service
{
    internal class TripService
    {
        private readonly MockTripDatabase tripDatabase = new();

        public Trip AddTrip(User user, Capital startingCapital, Capital destinationCapital)
        {
            Trip newTrip = new(user, startingCapital, destinationCapital);

            return tripDatabase.AddTrip(newTrip);
        }

        public Trip? GetTripByUsername(string username)
        {
            Trip? requestedTrip = tripDatabase.GetTripByUsername(username);

            return requestedTrip;
        }

        public Trip? GetTripById(long id)
        {
            return tripDatabase.GetTripById(id);
        }
    }
}
