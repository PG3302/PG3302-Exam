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
        public static TripModel AddTrip(TripModel newTrip)
        {
            using TravelDbContext travelDbContext = new();
            Trip trip = new();
            {
                trip.UserId = newTrip.User.Id;
				trip.DepartureCapitalId = newTrip.StartingCapital.Id;
                trip.ArrivalCapitalId = newTrip.DestinationCapital.Id;
            }
            Logger.LogInfo($" Adding trip: {trip}");
            travelDbContext.Add(trip);
            travelDbContext.SaveChanges();
            return MapTrip(trip);
        }

        public List<TripModel> GetTripAll()
        {
            using TravelDbContext travelDbContext = new();
            List<Trip> trips = travelDbContext.Trip.ToList();
            Logger.LogInfo("Attempting to get all trips...");
            return trips.Select(t => MapTrip(t)).ToList();
        }

        public TripModel GetTripById(int tripId)
        {
            using TravelDbContext travelDbContext = new();
            if (tripId <= 0)
            {
                Logger.LogError("Invalid input for trip data request: GetTripById (tripId is >0 or null)");
                return null;
            }
            Logger.LogInfo("Attempting to get Trip by Id: " + tripId);
            Trip? trip = travelDbContext.Trip.Find(tripId);
            return MapTrip(trip);
        }

        public List<TripModel> GetTripByUser(string email)
        {
            using TravelDbContext travelDbContext = new();
            Logger.LogInfo($"Attempting to get all trips with mail: {email}!");
            List<Trip> trips = travelDbContext.Trip.Where(t => t.User.Email == email).ToList();
            return trips.Select(t => MapTrip(t)).ToList();
        }

        public List<TripModel> GetTripsByCapital(Capital capital)
        {
            using TravelDbContext travelDbContext = new TravelDbContext();
            Logger.LogInfo($"Getting trips by capital: {capital}");
            List<Trip> trips = travelDbContext.Trip.Where(
                t => t.DepartureCapital.CapitalName == capital.CapitalName || 
                t.ArrivalCapital.CapitalName == capital.CapitalName).ToList();
            return trips.Select(t => MapTrip(t)).ToList();
        }
        public void DeleteTrip(int tripId)
        {
            using TravelDbContext travelDbContext = new();
            Trip trip = travelDbContext.Trip.First(trip => trip.Id == tripId);
            Logger.LogInfo($"Attempting to delete trip: {trip}");
            travelDbContext.Trip.Remove(trip);
            travelDbContext.SaveChanges();
        }
        public void EditTrip(int tripId, int userId, int departLocation, int arrivalLocation)
        {
            using TravelDbContext travelDbContext = new();
            Trip oldTrip = travelDbContext.Trip.First(trip => trip.Id == tripId);
            Logger.LogInfo($"Editing trip: {oldTrip}");

            oldTrip.UserId = userId;
            oldTrip.DepartureCapitalId = departLocation;
            oldTrip.ArrivalCapitalId = arrivalLocation;
            Logger.LogInfo($"New version of trip: {oldTrip}");

            travelDbContext.SaveChanges();
        }

        internal static TripModel? MapTrip(Trip? trip) {
            if (trip == null) {
                return null;
            }
            return new TripModel(
                trip.Id , 
                UserRepository.MapUser(trip.User) , 
                CapitalRepository.MapCapital(trip.DepartureCapital) , 
                CapitalRepository.MapCapital(trip.ArrivalCapital));
        }
    }
}
