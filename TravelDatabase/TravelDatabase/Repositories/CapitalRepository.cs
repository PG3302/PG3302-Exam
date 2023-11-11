using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories {
	public class CapitalRepository {
		public static int AddCapital(string name, Continent continent, decimal longitude, decimal latitude) {
			using var travelDbContext = new TravelDbContext();
			Capital capital = new Capital();
			{
				capital.CapitalName = name;
				capital.Continent = continent;
				capital.Longitude = longitude;
				capital.Latitude = latitude;
			}			
			travelDbContext.Capital.Add(capital);
			travelDbContext.SaveChanges();

			return capital.Id;
		}

		public List<Capital> GetAllCapitals() {
			using var travelDbContext = new TravelDbContext();
			return travelDbContext.Capital.ToList();
		}
		public Capital GetCapitalById(int id) {
			using var travelDbContext = new TravelDbContext();
			return travelDbContext.Capital.First(capital => capital.Id == id);
		}
		public void EditCapital(int capitalId , string name , Continent continent , decimal longitude , decimal latitude) {
			using var travelDbContext = new TravelDbContext();
			var oldCapital = travelDbContext.Capital.First(capital => capital.Id == capitalId);
			oldCapital.CapitalName = name;
			oldCapital.Continent = continent;
			oldCapital.Longitude = longitude;
			oldCapital.Latitude = latitude;
			travelDbContext.SaveChanges();
		}
	}
}
