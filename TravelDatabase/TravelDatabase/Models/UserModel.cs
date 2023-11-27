namespace TravelDatabase.Models {
	public class UserModel : Model {
		public long Id {
			get; set;
		}
		public string Name {
			get; private set;
		}
		public bool IsAdmin {
			get; set;
		}
		public string Email {
		get; set; }

		public UserModel(long id, string name , string email , bool isAdmin = false) {
			Id = id;
			Name = name;
			Email = email; 
			IsAdmin = isAdmin;
		}

		public override string ToString() {
			return $"{Id}: {Email}";
		}
	}
}
