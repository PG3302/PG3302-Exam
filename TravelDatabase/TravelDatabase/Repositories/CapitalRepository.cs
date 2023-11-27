using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories
{
    public class CapitalRepository {
		public static int AddCapital(string name, Continent continent, decimal longitude, decimal latitude) {
			using TravelDbContext travelDbContext = new();
			Capital capital = new();
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
			using TravelDbContext travelDbContext = new();
			return travelDbContext.Capital.ToList();
		}
		public Capital GetCapitalById(int id) {
			using TravelDbContext travelDbContext = new();
			return travelDbContext.Capital.First(capital => capital.Id == id);
		}
		public void EditCapital(int capitalId , string name , Continent continent , decimal longitude , decimal latitude) {
			using TravelDbContext travelDbContext = new();
			Capital oldCapital = travelDbContext.Capital.First(capital => capital.Id == capitalId);
			oldCapital.CapitalName = name;
			oldCapital.Continent = continent;
			oldCapital.Longitude = longitude;
			oldCapital.Latitude = latitude;
			travelDbContext.SaveChanges();
		}
	}
}
