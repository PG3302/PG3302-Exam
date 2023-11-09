using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories {
	public class InitDatabases {
		public static void InitFromCsv() {
			CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture) {
				HasHeaderRecord = true
			};
			using (StreamReader reader = new StreamReader(@"Resources\CountryCapitals.csv"))
			using (CsvReader csv = new CsvReader(reader , config)) {
				IEnumerable<Capital> records = csv.GetRecords<CsvRow>().Select(row => new Capital() {
					CapitalName = row.CapitalName ,
					Continent = row.ContinentName ,
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
	}
}
