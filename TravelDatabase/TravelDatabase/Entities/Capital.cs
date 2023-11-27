<<<<<<< Updated upstream
﻿using TravelDatabase.Data.DataType;
using TravelDatabase.Data.DataType.DataAccess.DataType;

namespace TravelDatabase.Entities
{
    public class Capital {
		public int Id {
			get; set;
		} //PK
		public string? CapitalName {
			get; set;
		}
		public Continent Continent {
			get; set;
		}
		public decimal Longitude {
			get; set;
		}
		public decimal Latitude {
			get; set;
		}
=======
﻿namespace TravelDatabase.Entities {
	public class Capital {
		public int Id {	get; set; } //PK
		public string? CapitalName { get; set; }
		public Continent Continent { get; set; }
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }
	}
	public enum Continent {
			Africa, Antarctica, Asia, Australia, CentralAmerica, Europe, NorthAmerica, SouthAmerica,
        value
    }
>>>>>>> Stashed changes
	}
}

