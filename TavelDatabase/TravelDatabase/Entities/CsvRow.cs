using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace TravelDatabase.Entities
{
    public class CsvRow
    {
        public string? CountryName { get; set; }
        public string? CapitalName { get; set; }
        public decimal? CapitalLatitude { get; set; }
        public decimal? CapitalLongitude { get; set; }
        public string? ContinentName { get; set; }
    }
}
