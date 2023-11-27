namespace TravelDatabase.Models {
	public class TripModel : Model {
        public int Id { get; set; }
        public UserModel User { get; private set; }
		public CapitalModel StartingCapital { get; private set; }
		public CapitalModel DestinationCapital { get; private set; }
		public double Price { get; private set; }

		public TripModel(int id, UserModel user , CapitalModel startingCapital , CapitalModel destinationCapital) {
			Id = id;
			User = user;
			StartingCapital = startingCapital;
			DestinationCapital = destinationCapital;

			Price = startingCapital.Coordinate - destinationCapital.Coordinate;
		}

		public override string ToString() {
			return $"{User} -- {StartingCapital} -> {DestinationCapital} ({Price})";
		}
	}
}
