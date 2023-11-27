using TravelDatabase.Models;

namespace TravelPlanner.TravelPlannerApp.Service {
	internal class TripService
    {
        //private readonly MockTripDatabase tripDatabase = new();

        public TripModel AddTrip(UserModel user, CapitalModel start, CapitalModel destination)
        {
            TripModel newTrip = new(user, start, destination)
            {
                Price = CalculateTripPrice(start, destination)
            };

            return tripDatabase.AddTrip(newTrip);
        }

        public TripModel? GetTripByUsername(string username)
        {
            TripModel? requestedTrip = tripDatabase.GetTripByUsername(username);

            return requestedTrip;
        }

        public TripModel? GetTripById(long id)
        {
            return tripDatabase.GetTripById(id);
        }

        private int CalculateTripPrice(CapitalModel start, CapitalModel destination)
        {
            double distance = start.Coordinate - destination.Coordinate;
            double absoluteDistance = Math.Abs(distance);
            int price = Convert.ToInt32(absoluteDistance * 100);

            return price;
        }
    }
}
