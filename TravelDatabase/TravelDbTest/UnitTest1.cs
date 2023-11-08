

namespace TravelDbTest {
	public class Tests {
		[SetUp]
		public void Setup() {
		}

		[Test]
		public void AddTestUserToDbReturnNameOfUser() {
			int userId = TravelDatabase.Program.AddUser("test", new TravelDatabase.Entities.Capital(), 0);
			Assert.AreEqual(userId , 1);
		} // test DB can be found \\TravelDatabase\TravelDbTest\bin\Debug\net6.0\Resources

		[Test]
		public void AddUserOutsideAdminSpecsFails() {
			Assert.Throws<Exception>(() => TravelDatabase.Program.AddUser("test" , new TravelDatabase.Entities.Capital() , 3)); //throws<exception> thinks it's recieving an int. lambda fix
		}
	}
}