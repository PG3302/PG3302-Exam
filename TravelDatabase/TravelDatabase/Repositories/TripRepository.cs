using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories {
	public class TripRepository {
		public static void AddTrip(int userId , int departlocation , int arrivalLocation) {
			using TravelDbContext db = new();
			Trip trip = new() {
				UserId = userId ,
				DepartureId = departlocation ,
				ArrivalId = arrivalLocation ,
			};
			db.Add(trip);
			db.SaveChanges();
		}
	}
}
