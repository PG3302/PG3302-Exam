using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories {
	public class CapitalRepository {

		public List<Capital> GetAllCapitals() {
			using var travelDbContext = new TravelDbContext();
			return travelDbContext.Capital.ToList();
		}
		public Capital GetCapitalById(int id) {
			using var travelDbContext = new TravelDbContext();
			return travelDbContext.Capital.First(capital => capital.Id == id);
		}
	}
}
