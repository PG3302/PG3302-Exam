using TravelPlanner.TravelPlannerApp.Data.DataType;

namespace TravelPlanner.TravelPlannerApp.Data.Objects
{
    public class Location
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public Coordinate Coordinate { get; private set; }
        public Continent Continent { get; private set; }

        public Location(string name, Coordinate coordinate, Continent continent)
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
