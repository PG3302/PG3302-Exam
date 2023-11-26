using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Data.Models
{
    public class Capital : Model
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public Coordinate Coordinate { get; private set; }
        public Continent Continent { get; private set; }

        public Capital(string name, Coordinate coordinate, Continent continent)
        {
            Name = name;
            Coordinate = coordinate;
            Continent = continent;
        }

        public override string ToString()
        {
            return $"{Id} [{Continent}]: {Name} ({Coordinate.Longitude} {Coordinate.Latitude})";
        }
    }
}