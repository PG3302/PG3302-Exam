using TravelDatabase.Data.DataType;

namespace TravelDatabase.Models
{
    public class CapitalModel : Model {
        public int Id { get; private set; }
        public string Name { get; private set; }
		public Coordinate Coordinate { get; private set; }
		public Continent Continent { get; private set; }

		public CapitalModel(int id, string name , Coordinate coordinate , Continent continent) {
			Id = id;
			Name = name;
			Coordinate = coordinate;
			Continent = continent;
		}

		public override string ToString() {
			return $"[{Continent}]: {Name} ({Coordinate.Longitude} {Coordinate.Latitude})";
		}
	}
}