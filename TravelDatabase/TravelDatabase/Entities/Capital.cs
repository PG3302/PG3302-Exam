﻿using TravelPlanner.TravelPlannerApp.Data.DataType;

namespace TravelDatabase.Entities {
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
	}
}

