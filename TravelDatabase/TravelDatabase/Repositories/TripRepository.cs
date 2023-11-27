using Microsoft.EntityFrameworkCore;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories
{
    public class TripRepository {
		public static void AddTrip(int userId , int departLocation , int arrivalLocation) {
			using TravelDbContext travelDbContext = new();
			Trip trip = new() {
				UserId = userId ,
				DepartureId = departLocation ,
				ArrivalId = arrivalLocation ,
			};
			travelDbContext.Add(trip);
			travelDbContext.SaveChanges();
		}
		public List<Trip> GetAllUserTrips(int userId) {
			using TravelDbContext travelDbContext = new();
			return travelDbContext.Trip.Where(trip => trip.UserId == userId ).ToList();
		}
		public void DeleteTrip(int tripId) {
			using TravelDbContext travelDbContext = new();
			Trip trip = travelDbContext.Trip.First(trip => trip.Id == tripId);
			travelDbContext.Trip.Remove(trip);
			travelDbContext.SaveChanges();
		}
		public void EditTrip(int tripId, int userId, int departLocation, int arrivalLocation) {
			using TravelDbContext travelDbContext = new();
			Trip oldTrip = travelDbContext.Trip.First(trip => trip.Id == tripId);
			oldTrip.UserId = userId;
			oldTrip.DepartureId = departLocation;
			oldTrip.ArrivalId = arrivalLocation;
			travelDbContext.SaveChanges();
		}
		public Trip? GetSingleTrip(int tripId) 
			{
			using TravelDbContext travelDbContext = new();
			if (tripId <= 0) {
				Console.WriteLine("Invalid input for trip data request");
				return null;
			}

			return travelDbContext.Trip
				.Where(t => t.Id == tripId)
				.Include(t => t.Arrival)
				.Include(t => t.Departure)
				.Include(t => t.User)
				.FirstOrDefault();
		}
		public List<Trip> GetAllTrips() {
			using TravelDbContext travelDbContext = new();
			return travelDbContext.Trip
				.Include(t => t.Arrival)
				.Include(t => t.Departure)
				.Include(t => t.User)
				.ToList();
		}
	}
}
