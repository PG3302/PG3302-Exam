using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDatabase.Entities {
	public class Capital {
		public int Id {	get; set; } //PK
		public string? CapitalName { get; set; }
		public Continent Continent { get; set; }
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }
	}
	public enum Continent {
			Africa, Antarctica, Asia, Australia, CentralAmerica, Europe, NorthAmerica, SouthAmerica
		}
	}
