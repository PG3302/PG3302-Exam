using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Diagnostics;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace TravelDatabase
{
    public class Program {
		static void Main() {
			InitFromCsv();

		}

		static void InitFromCsv() {
			CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture) {
				HasHeaderRecord = true
			};
			using (StreamReader reader = new StreamReader(@"Resources\CountryCapitals.csv"))
			using (CsvReader csv = new CsvReader(reader , config)) {
				IEnumerable<Capital> records = csv.GetRecords<CsvRow>().Select(row => new Capital() {
					CapitalName = row.CapitalName ,
					Continent = row.ContinentName ,
					Longitude = row.CapitalLongitude ??0, //substitutes with 0 if value is NULL
					Latitude = row.CapitalLatitude ??0,
				});

				using (TravelDbContext db = new()) {

					List<Capital> items = records.ToList();

					db.Capital.AddRange(items);
					db.SaveChanges();

					Console.WriteLine(DateTime.Now);
				}
			}
		}
		public static int AddUser(string username, Capital city, int admin) {
			if (admin == 0 || admin == 1) {
				User user = new();{
					user.Name = $"{username}";
					user.CityId = city.Id; //needs city selected from Capital table
					user.Admin = admin;
				}
				using TravelDbContext db = new();
				db.User.Add(user);
				db.SaveChanges();

				return user.Id;
			}throw new Exception("Admin value not allowed");
		}
		static void AddTrip(int userId , int departlocation , int arrivalLocation) {
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
