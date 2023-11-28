using TravelDatabase.Models;
using TravelDatabase.Repositories;

// Database saves here: \\TravelDatabase\TravelDatabase\bin\Debug\net6.0\Resources

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
