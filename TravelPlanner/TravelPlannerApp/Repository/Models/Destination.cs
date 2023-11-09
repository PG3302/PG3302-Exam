using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.TravelPlannerApp.Repository.Models
{
    public  class Destination {
        public string Name { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
        public double PriceRating { get; set; } = double.MinValue;
        public string Airport {  get; set; } = string.Empty;
        public int Population { get; set; } = 0;

        public override string ToString()
        {
            return $"City name: ${CityName} \nLocated in: ${CountryName}, ${Continent}\nPrice rating: ${PriceRating} | Airport: ${Airport} | Safety rating?";
        }


    }
}
