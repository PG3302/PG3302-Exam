using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Models;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal class TripDatabase : ITravelDb
    {
       
        private readonly List<Trip> _tripList = new();
        private int _tripCurrentId = 0;

        public TripDatabase()
        {

        }

        public Trip AddTrip(Trip trip)
        {
            ConnectDatabase();

            //Mock
            trip.Id = _tripCurrentId;
            _tripCurrentId++;
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

        public void ExecuteNonQuery(string query)
        {
            throw new NotImplementedException();
        }
    }
}
