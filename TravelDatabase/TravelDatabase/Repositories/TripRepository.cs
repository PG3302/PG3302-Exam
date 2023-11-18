using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories {
	public class TripRepository {
		public static void AddTrip(int userId , int departLocation , int arrivalLocation) {
			using var travelDbContext = new TravelDbContext();
			Trip trip = new() {
				UserId = userId ,
				DepartureId = departLocation ,
				ArrivalId = arrivalLocation ,
			};
			travelDbContext.Add(trip);
			travelDbContext.SaveChanges();
		}
		public List<Trip> GetAllUserTrips(int userId) {
			using var travelDbContext = new TravelDbContext();
			return travelDbContext.Trip.Where(trip => trip.UserId == userId ).ToList();
		}
		public void DeleteTrip(int tripId) {
			using var travelDbContext = new TravelDbContext();
			var trip = 	travelDbContext.Trip.First(trip => trip.Id == tripId);
			travelDbContext.Trip.Remove(trip);
			travelDbContext.SaveChanges();
		}
		public void EditTrip(int tripId, int userId, int departLocation, int arrivalLocation) {
			using var travelDbContext = new TravelDbContext();
			var oldTrip = travelDbContext.Trip.First(trip => trip.Id == tripId);
			oldTrip.UserId = userId;
			oldTrip.DepartureId = departLocation;
			oldTrip.ArrivalId = arrivalLocation;
			travelDbContext.SaveChanges();
		}
		public Trip? GetSingleTrip(int tripId)
        {
			using var travelDbContext = new TravelDbContext();
            Trip? trip = null;

            if (tripId <= 0)
            {
                Console.WriteLine("Invalid input for trip data request");
                return trip;
            }
            using SqliteConnection con = new(_conString);
            con.Open();

            //Add so we get all attributes of Trip object

            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Trip";
            cmd.CommandText += @"WHERE Id = $Id;";


            cmd.Parameters.AddWithValue("$Id", tripId);

            using SqliteDataReader reader = cmd.ExecuteReader();
            trip.Id = reader.GetInt32(0);
            trip.UserId = reader.GetInt32(1);
            //trip.Arrival = reader.GetString(2);
            //trip.Arrival = reader.GetString(3);

            con.Close();
            return trip; //Edit so it returns correct values for each instance
        }
	}
}
