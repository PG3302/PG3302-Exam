using NUnit.Framework.Legacy;
using TravelDatabase.Data.DataType;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Entities;
using TravelDatabase.Models;
using TravelDatabase.Repositories;

// test DB can be found \\TravelDatabase\TravelDbTest\bin\Debug\net6.0\Resources
namespace TravelDbTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            using TravelDbContext travelDbContext = new();
            travelDbContext.RemoveRange(travelDbContext.Capital);
            travelDbContext.RemoveRange(travelDbContext.User);
            travelDbContext.SaveChanges();
            InitDatabase.InitFromCsv();
         
        }

        #region [User tests]
        [Test]
        public void AddUserTest()
        {
            Setup();
            UserRepository userRepo = new UserRepository();
            UserModel newUser = userRepo.AddUser(
                new UserModel(null, "TestUser", "testUser@Test.com", false)
            );
            Assert.That(newUser.Id, Is.Not.Null);
        }
        [Test]
        public void GetUserAllTest()
        {
            Setup();
            UserRepository userRepo = new UserRepository();
            UserModel newUser = userRepo.AddUser(
                new UserModel(null, "TestUser", "testUser@Test.com", false)
            );
            List<UserModel?> users = userRepo.GetUserAll();
            Assert.That(users.Count, Is.AtLeast(1));
        }


        [Test]
        public void MapUser_WhenUserIsNotNull()
        {
            Setup();
            User user = new()
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com",
                Admin = 1
            };
            UserModel result = MapUser(user);

            ClassicAssert.NotNull(result);
            Assert.That(user.Id, Is.EqualTo( result.Id));
            Assert.That(user.Name, Is.EqualTo(result.Name));
            Assert.That(user.Email, Is.EqualTo(result.Email));
            Assert.That(user.Admin == 1, Is.EqualTo(result.IsAdmin));
        }

        [Test]
        public void MapUser_WhenUserIsNull()
        {
            User user = null;
            UserModel result = MapUser(user);
            ClassicAssert.IsNull(result);
        }

        internal static UserModel? MapUser(User? user)
        {
            if (user == null)
            {
                return null;
            }
            return new UserModel(user.Id, user.Name!, user.Email!, user.Admin == 1);
        }
        #endregion

        [Test]
        public void AddCapitalTest()
        {
            Setup();
            CapitalRepository capitalRepo = new CapitalRepository();
            CapitalModel newCapital = capitalRepo.AddCapital(
                new CapitalModel(null, "test", 0, 1, Continent.Oceania)
            );
            List<CapitalModel> allCapitals = new CapitalRepository().GetCapitalAll();
            Assert.That(allCapitals.Any());
        }

        internal static CapitalModel? MapCapital(Capital capital)
        {
            if (capital == null)
            {
                return null;
            }
            return new CapitalModel(
                capital.Id,
                capital.CapitalName,
                capital.Longitude,
                capital.Latitude,
                capital.Continent
            );
        }

        internal static TripModel? MapTrip(Trip? trip)
        {
            if (trip == null)
            {
                return null;
            }
            return new TripModel(
                trip.Id,
                MapUser(trip.User),
                MapCapital(trip.DepartureCapital),
                MapCapital(trip.ArrivalCapital)
            );
        }
    }
}
