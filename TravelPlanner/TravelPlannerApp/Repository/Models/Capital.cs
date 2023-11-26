using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Data.Models
{
    public class Capital : Model
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Coordinate Coordinate { get; set; }
        public Continent Continent { get; set; }

        public Capital(string name, Coordinate coordinate, Continent continent)
        {
            Name = name;
            Coordinate = coordinate;
            Continent = continent;
        }

        public string LatCoordToString(Coordinate coord)
        {
            string output = coord.Latitude.ToString();
            return output;
        }

        public string LongCoordToString(Coordinate coord)
        {
            string output = coord.Longitude.ToString();
            return output;
        }

        public override string ToString()
        {
            return $"{Id} [{Continent}]: {Name} ({Coordinate.Longitude} {Coordinate.Latitude})";
        }
    }
}