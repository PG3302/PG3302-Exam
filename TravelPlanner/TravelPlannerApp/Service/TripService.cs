using TravelPlanner.TravelPlannerApp.Data.Objects;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service
{
    internal class TripService
    {
        private readonly MockTripDatabase tripDatabase = new();

        public Trip AddTrip(User user, Capital start, Capital destination)
        {
            Trip newTrip = new(user, start, destination)
            {
                Price = CalculateTripPrice(start, destination)
            };

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

        private int CalculateTripPrice(Capital start, Capital destination)
        {
            int distance = (int)(start.Coordinate - destination.Coordinate);
            int price = distance * 100;

            return price;
        }
    }
}
