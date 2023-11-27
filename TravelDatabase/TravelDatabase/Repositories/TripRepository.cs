using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories
{
    public class TripRepository
    {
        public static void AddTrip(int userId, int departLocation, int arrivalLocation)
        {
            using TravelDbContext travelDbContext = new();
            Trip trip = new()
            {
                UserId = userId,
                DepartureId = departLocation,
                ArrivalId = arrivalLocation,
            };
            travelDbContext.Add(trip);
            travelDbContext.SaveChanges();
        }
        public List<Trip> GetTripByUser(int userId)
        {
            using TravelDbContext travelDbContext = new();
            Logger.LogInfo("Attempting to get all trips from user ID: " + userId);
            return travelDbContext.Trip.Where(trip => trip.UserId == userId).ToList();
        }

        public List<Trip> GetTripsByCapital(Capital capital)
        {
            using TravelDbContext travelDbContext = new TravelDbContext();
            Logger.LogInfo(
               "Getting trips by capital: " + capital.CapitalName
                );
            return travelDbContext.Trip
                .Include(t => t.Departure)
                .Include(t => t.Arrival)
                .Where(t => t.DepartureId == capital.Id || t.ArrivalId == capital.Id)
                .ToList();
        }

        public List<Trip> GetTripAll()
        {
            using TravelDbContext travelDbContext = new();
            Logger.LogInfo("Attempting to get all trips...");
            return travelDbContext.Trip
                .Include(t => t.Arrival)
                .Include(t => t.Departure)
                .Include(t => t.User)
                .ToList();
        }
        public void DeleteTrip(int tripId)
        {
            using TravelDbContext travelDbContext = new();
            Trip trip = travelDbContext.Trip.First(trip => trip.Id == tripId);
            travelDbContext.Trip.Remove(trip);
            travelDbContext.SaveChanges();
        }
        public void EditTrip(int tripId, int userId, int departLocation, int arrivalLocation)
        {
            using TravelDbContext travelDbContext = new();
            Trip oldTrip = travelDbContext.Trip.First(trip => trip.Id == tripId);
            oldTrip.UserId = userId;
            oldTrip.DepartureId = departLocation;
            oldTrip.ArrivalId = arrivalLocation;
            travelDbContext.SaveChanges();
        }
        public Trip? GetTripById(int tripId)
        {
            using TravelDbContext travelDbContext = new();
            if (tripId <= 0)
            {
                Logger.LogError("Invalid input for trip data request: GetTripById (tripId is >0 or null)");
                return null;
            }
            Logger.LogInfo("Attempting to get Trip by Id");
            return travelDbContext.Trip
                .Where(t => t.Id == tripId)
                .Include(t => t.Arrival)
                .Include(t => t.Departure)
                .Include(t => t.User)
                .FirstOrDefault();
        }
    }
}
