

using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Repositories;

namespace TravelDbTest
{
	/*
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
			int userId = UserRepository.AddUser(new TravelDatabase.Models.UserModel(1, "test" , "test@test.com" , false));
			Assert.That(1 , Is.EqualTo(userId));
		} // test DB can be found \\TravelDatabase\TravelDbTest\bin\Debug\net6.0\Resources

		[Test]
		public void AddCapitalTest() {
			int capitalId = CapitalRepository.AddCapital("test" , 0 , 1 , 2);
			var allCapitals = new CapitalRepository().GetCapitalAll();
			Assert.That(allCapitals.Any(capital => capital.CapitalName.Contains("test")));
		}
	}
		*/
}