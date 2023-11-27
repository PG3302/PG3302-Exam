using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Entities;
using TravelDatabase.Models;
using Microsoft.EntityFrameworkCore;
using TravelDatabase.Data.DataType;
using System.Reflection.Metadata.Ecma335;


namespace TravelDatabase.Repositories
{
    public class CapitalRepository {
		public static int AddCapital(CapitalModel newCapital) {
			using TravelDbContext travelDbContext = new();
			if (travelDbContext.Capital.Any(c=>c.CapitalName == newCapital.Name)) {
			}
			Capital capital = new();
			{
				capital.CapitalName = newCapital.Name;
				capital.Continent = newCapital.Continent;
				capital.Longitude = (decimal)newCapital.Coordinate.Longitude;
				capital.Latitude = (decimal)newCapital.Coordinate.Latitude;
			}			
			travelDbContext.Capital.Add(capital);
			travelDbContext.SaveChanges();

			return capital.Id;
		}

		public List<CapitalModel> GetCapitalAll() 
		{
			using TravelDbContext travelDbContext = new();
			List<Capital> capitals = travelDbContext.Capital.ToList();
			return capitals.Select(c => MapCapital(c)).ToList();
		}

		public CapitalModel? GetCapitalById(int capitalId) 
			{
			using TravelDbContext travelDbContext = new();
			Capital? capital = travelDbContext.Capital.Find(capitalId);
			return MapCapital(capital);
		}

		public CapitalModel GetCapitalByName(string name)
		{
			using TravelDbContext travelDbContext = new();
			Capital? capital = travelDbContext.Capital.First(capital => capital.CapitalName == name);
			return MapCapital(capital);
		}
        public List<CapitalModel> GetCapitalsByContinent(Continent continent)
        {
            using TravelDbContext travelDbContext = new TravelDbContext();
			List<Capital> capitals = travelDbContext.Capital.Include(c => c.Continent).Where(c => c.Continent == continent).ToList();
            return capitals.Select(c=>MapCapital(c)).ToList();
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

		internal static CapitalModel? MapCapital(Capital capital) {
			if (capital == null) {
				return null;
			}
			return new CapitalModel(
				capital.Id ,
				capital.CapitalName , 
				capital.Longitude , 
				capital.Latitude , 
				capital.Continent);
		}
		
	}
}
