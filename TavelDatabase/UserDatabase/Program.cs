using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Diagnostics;
using UserDatabase.DataAccess.SqLite;
using UserDatabase.Entities;

namespace UserDatabase {
	public class Program {
		static void Main() {
		}
		public static int AddUser(string username, string city, int admin) {
			if (admin == 0 || admin == 1) {
				User user = new();{
					user.Name = $"{username}";
					user.City = $"{city}"; //change to read from locationDb ref via PK ID. add to DBcontext n User class
					user.Admin = admin;
				}
				using UserDbContext db = new();
				db.User.Add(user);
				db.SaveChanges();

				return user.Id;
			}throw new Exception("Admin value not allowed");
		}
		static void AddTrip(int userId , int departlocation , int arrivalLocation) {
			using UserDbContext db = new();
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
