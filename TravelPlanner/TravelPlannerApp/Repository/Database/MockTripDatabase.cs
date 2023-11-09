using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Objects;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal class MockTripDatabase : IMockDatabase
    {
        private readonly List<Trip> _tripList = new();
        private long _mockCurrentId = 0;

        public MockTripDatabase()
        {

        }

        public Trip AddTrip(Trip trip)
        {
            ConnectDatabase();

            //Mock
            trip.Id = _mockCurrentId;
            _mockCurrentId++;
            _tripList.Add(trip);

            Logger.LogInfo($"Trip {trip.StartingCapital} -> {trip.DestinationCapital} added.");

            //Statement
            return (trip);
        }

        public Trip? GetTripByUsername(string username)
        {
            Trip? requestedTrip = _tripList.Find(t => t.User.Username == username);

            return requestedTrip;
        }

        public Trip? GetTripById(long id)
        {
            Trip? requestedTrip = _tripList.Find(i => i.Id == id);

            return requestedTrip;
        }

        public void ConnectDatabase()
        {
            Console.WriteLine("Trip Database Connected...");
        }

        public void DisconnectDatabase()
        {
            Console.WriteLine("Trip Database Disconnected...");
        }
    }
}
