using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;
using TravelDatabase.Models;

namespace TravelDatabase.Repositories
{
    public class TripRepository
    {
        private static readonly object _lock = new object();

        public TripModel AddTrip(TripModel newTrip)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Trip trip = new();
                {
                    trip.UserId = newTrip.User.Id ?? -1;
                    trip.DepartureCapitalId = newTrip.StartingCapital.Id.Value;
                    trip.ArrivalCapitalId = newTrip.DestinationCapital.Id.Value;
                }
                Logger.LogInfo($"Attempting to add trip: {newTrip}");

                travelDbContext.Add(trip);
                Logger.LogInfo($"Trip: {newTrip} added to db");

                travelDbContext.SaveChanges();
                return GetTripById(trip.Id);
            }
        }

        public List<TripModel> GetTripAll()
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                List<Trip> trips = travelDbContext
                    .Trip
                    .Include(t => t.User)
                    .Include(t => t.DepartureCapital)
                    .Include(t => t.ArrivalCapital)
                    .ToList();
                Logger.LogInfo("Attempting to get all trips...");

                return trips.Select(t => MapTrip(t)).ToList();
            }
        }

        public TripModel GetTripById(int tripId)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                if (tripId <= 0)
                {
                    Logger.LogError(
                        "Invalid input for trip data request: GetTripById (tripId is >0 or null)"
                    );

                    return null;
                }
                Logger.LogInfo("Attempting to get Trip by Id: " + tripId);

                Trip? trip = travelDbContext
                    .Trip
                    .Include(t => t.User)
                    .Include(t => t.ArrivalCapital)
                    .Include(t => t.DepartureCapital)
                    .First(x => x.Id == tripId);
                return MapTrip(trip);
            }
        }

        public List<TripModel> GetTripByUser(string email)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Logger.LogInfo($"Attempting to get all trips with mail: {email}!");

                List<Trip> trips = travelDbContext
                    .Trip
                    .Include(t => t.User)
                    .Include(t => t.DepartureCapital)
                    .Include(t => t.ArrivalCapital)
                    .Where(t => t.User.Email == email)
                    .ToList();
                return trips.Select(t => MapTrip(t)).ToList();
            }
        }

        public List<TripModel> GetTripByCapital(int capitalId)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new TravelDbContext();
                Logger.LogInfo($"Getting trips by capitalId: {capitalId}");

                List<Trip> trips = travelDbContext
                    .Trip
                    .Include(t => t.User)
                    .Include(t => t.DepartureCapital)
                    .Include(t => t.ArrivalCapital)
                    .Where(
                        t => t.DepartureCapital.Id == capitalId || t.ArrivalCapital.Id == capitalId
                    )
                    .ToList();
                return trips.Select(t => MapTrip(t)).ToList();
            }
        }

        public void DeleteTrip(int tripId)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Trip trip = travelDbContext
                    .Trip
                    .Include(t => t.User)
                    .Include(t => t.DepartureCapital)
                    .Include(t => t.ArrivalCapital)
                    .First(trip => trip.Id == tripId);
                Logger.LogInfo($"Attempting to delete trip: {trip}");

                travelDbContext.Trip.Remove(trip);
                Logger.LogInfo($"Removed {trip} from database");

                travelDbContext.SaveChanges();
            }
        }

        public void EditTrip(int tripId, int userId, int departLocation, int arrivalLocation)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Trip oldTrip = travelDbContext
                    .Trip
                    .Include(t => t.User)
                    .Include(t => t.DepartureCapital)
                    .Include(t => t.ArrivalCapital)
                    .First(trip => trip.Id == tripId);
                Logger.LogInfo($"Editing trip: {oldTrip}");

                oldTrip.UserId = userId;
                oldTrip.DepartureCapitalId = departLocation;
                oldTrip.ArrivalCapitalId = arrivalLocation;
                Logger.LogInfo($"New version of trip: {oldTrip}");

                travelDbContext.SaveChanges();
            }
        }

        internal static TripModel? MapTrip(Trip? trip)
        {
            if (trip == null)
            {
                Logger.LogError($"Trip: ({trip}) could not be mapped to TripModel");
                return null;
            }
            return new TripModel(
                trip.Id,
                UserRepository.MapUser(trip.User),
                CapitalRepository.MapCapital(trip.DepartureCapital),
                CapitalRepository.MapCapital(trip.ArrivalCapital)
            );
        }
    }
}
