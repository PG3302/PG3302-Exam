using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;
using TravelPlanner.TravelPlannerApp.Data.DataType;

namespace TravelDatabase.Repositories {
	public class InitDatabase {
		public static void InitFromCsv() {
			CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture) {
				HasHeaderRecord = true
			};
			using (StreamReader reader = new StreamReader(@"Resources\CountryCapitals.csv"))
			using (CsvReader csv = new CsvReader(reader , config)) {
				IEnumerable<Capital> records = csv.GetRecords<CsvRow>().Select(row => new Capital() {
					CapitalName = row.CapitalName ,
					Continent = MakeStringEnum(row.ContinentName) ,
					Longitude = row.CapitalLongitude ?? 0 , //substitutes with 0 if value is NULL
					Latitude = row.CapitalLatitude ?? 0 ,
				});

				using (TravelDbContext db = new()) {

					List<Capital> items = records.ToList();

					db.Capital.AddRange(items);
					db.SaveChanges();

					Console.WriteLine(DateTime.Now);
				}
			}
		}
		private static Continent MakeStringEnum(string? str) {
			switch (str) {
				case "Africa": return Continent.Africa;
				case "Antarctica": return Continent.Antarctica;
				case "Asia": return Continent.Asia;
				case "Europe": return Continent.Europe;
				case "Australia": return Continent.Oceania;
				case "North America": return Continent.NorthAmerica;
				case "South America": return Continent.SouthAmerica;
				case "Central America": return Continent.CentralAmerica;
				default: throw new Exception("No Continent matched");
					//can be shortened w parse enum to string w no space for N /S / C America
			}
		}
	}
}
