namespace TravelDatabase.Models {
	public class UserModel : Model {
		public string Name {
			get; private set;
		}
		public bool IsAdmin {
			get; private set;
		}
		public string Email {
			get; private set; 
		}

		public UserModel(string name , string email , bool isAdmin = false) {
			Name = name;
			Email = email; 
			IsAdmin = isAdmin;
		}

		public override string ToString() {
			return $"{Email}: ${Name} " + IsAdmin ?? "(Admin)";
		}
	}
}
