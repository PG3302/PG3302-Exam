namespace TravelDatabase.Models {
	public class TripModel : Model {
		public UserModel User {
			get; private set;
		}
		public CapitalModel StartingCapital {
			get; private set;
		}
		public CapitalModel DestinationCapital {
			get; private set;
		}
		public int Price {
			get; set;
		}

		public TripModel(UserModel user , CapitalModel startingCapital , CapitalModel destinationCapital) {
			User = user;
			StartingCapital = startingCapital;
			DestinationCapital = destinationCapital;
		}

		public override string ToString() {
			return $"{User} -- {StartingCapital} -> {DestinationCapital} ({Price})";
		}
	}
}
