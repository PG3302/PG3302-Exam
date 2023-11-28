using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelDatabase {
	public class Program {
		static void Main() {
			InitDatabase.InitFromCsv();
			UserRepository userRepo = new UserRepository();
			userRepo.AddUser(new UserModel(null , "TestUser" , "testUser@Test.com" , false));
			userRepo.AddUser(new UserModel(null , "TestAdmin" , "testAdmin@Test.com" , true));
		}
	}
}
