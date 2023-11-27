

using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Repositories;

namespace TravelDbTest
{
    public class Tests {
		[SetUp]
		public void Setup() {
			using TravelDbContext travelDbContext = new();
			travelDbContext.RemoveRange(travelDbContext.Capital);
			travelDbContext.SaveChanges();
			InitDatabase.InitFromCsv();
		}

		[Test]
		public void AddUserTest() {
			int userId = UserRepository.AddUser("test", new CapitalRepository().GetAllCapitals().First(), 0);
			Assert.That(1 , Is.EqualTo(userId));
		} // test DB can be found \\TravelDatabase\TravelDbTest\bin\Debug\net6.0\Resources

		[Test]
		public void AddUserOutsideAdminSpecsFails() {
			Assert.Throws<Exception>(() => UserRepository.AddUser("test" , new TravelDatabase.Entities.Capital() , 3)); 
			//throws<exception> thinks it's recieving an int. lambda fix
		}
		[Test]
		public void AddCapitalTest() {
			int capitalId = CapitalRepository.AddCapital("test" , 0 , 1 , 2);
			var allCapitals = new CapitalRepository().GetAllCapitals();
			Assert.That(allCapitals.Any(capital => capital.CapitalName.Contains("test")));
		}
	}
}