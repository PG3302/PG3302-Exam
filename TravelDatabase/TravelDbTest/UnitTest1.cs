

using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Entities;
using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelDbTest
{
	
    public class Tests {
		[SetUp]
		public void Setup() {
			using TravelDbContext travelDbContext = new();
			travelDbContext.RemoveRange(travelDbContext.Capital);
			travelDbContext.RemoveRange(travelDbContext.User);
			travelDbContext.SaveChanges();
			InitDatabase.InitFromCsv();
		}

		[Test]
		public void AddUserTest() {
			Setup();
			UserRepository userRepo = new UserRepository();
			UserModel newUser = userRepo.AddUser(new UserModel(null , "TestUser" , "testUser@Test.com" , false));
			Assert.That(newUser.Id , Is.Not.Null);
		} // test DB can be found \\TravelDatabase\TravelDbTest\bin\Debug\net6.0\Resources
		/*
		[Test]
		public void AddCapitalTest() {
			int capitalId = CapitalRepository.AddCapital("test" , 0 , 1 , 2);
			var allCapitals = new CapitalRepository().GetCapitalAll();
			Assert.That(allCapitals.Any(capital => capital.CapitalName.Contains("test")));
		}
		*/
		internal static UserModel? MapUser(User? user) {
			if (user == null) {
			//	Logger.LogError($"Failed to map {user} to UserModel");
				return null;
			}
			//Logger.LogInfo($"Mapping user: {user} to UserModel");
			return new UserModel(
				user.Id ,
				user.Name! ,
				user.Email! ,
				user.Admin == 1);
		}
		internal static CapitalModel? MapCapital(Capital capital) {
			if (capital == null) {
			//	Logger.LogError("Attempted to map a capital. " + capital.ToString() + ". Input == NULL");
				return null;
			}
			return new CapitalModel(
				capital.Id ,
				capital.CapitalName ,
				capital.Longitude ,
				capital.Latitude ,
				capital.Continent);
		}
		internal static TripModel? MapTrip(Trip? trip) {
			if (trip == null) {
			//	Logger.LogError($"Trip: ({trip}) could not be mapped to TripModel");
				return null;
			}
			return new TripModel(
				trip.Id ,
				MapUser(trip.User) ,
				MapCapital(trip.DepartureCapital) ,
				MapCapital(trip.ArrivalCapital));
		}
	}
}