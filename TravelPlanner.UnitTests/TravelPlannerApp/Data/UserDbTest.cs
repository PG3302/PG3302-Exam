using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Service;

namespace TravelDatabase.DataAccess.SqLite.Tests
{
    [TestFixture]
    public class TravelDbContextTests
    {
        private TravelDbContext _dbContext;
        private string _testDatabasePath;

        [SetUp]
        public void Setup()
        {
            // Use a unique database for each test to avoid interference
            _testDatabasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.db");

            var options = new DbContextOptionsBuilder<TravelDbContext>()
                .UseSqlite($"Data Source={_testDatabasePath}")
                .Options;

            _dbContext = new TravelDbContext(options);

            // Ensure the database is created
            _dbContext.Database.EnsureCreated();
        }

        [Test]
        public void AddUser_ShouldAddUserToDatabase()
        {
            var testCapital = new Capital("TestCapital", new Coordinate(0.0, 0.0), Continent.Asia);
            var newUser = _dbContext.AddUser("TestUser", testCapital, isAdmin: false, email: "test@example.com");
            // Act
            _dbContext.AddUser(newUser);

            // Assert
            var addedUser = _dbContext.User.FirstOrDefault(u => u.Username == "TestUser");
            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Username, addedUser.Username);
            Assert.AreEqual(newUser.Address, addedUser.Address);
            Assert.AreEqual(newUser.Email, addedUser.Email);
        }

        [Test]
        public void GetUserById_ShouldRetrieveUserFromDatabase()
        {
            // Arrange
            var newUser = new User
            {
                Username = "TestUser",
                Address = new Capital
                {
                    Name = "TestCity",
                    Continent = Continent.Asia,
                    Coordinate = new Coordinate(0, 0)
                },
                IsAdmin = false,                
                Email = "testuser@example.com"
            };

            _dbContext.AddUser(newUser);

            // Act
            var retrievedUser = _dbContext.GetUserById(newUser.Id);

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(newUser.Id, retrievedUser.Id);
            Assert.AreEqual(newUser.Username, retrievedUser.Username);
            Assert.AreEqual(newUser.Address, retrievedUser.Address);
            Assert.AreEqual(newUser.Email, retrievedUser.Email);
        }

        [Test]
        public void GetUserByUsername_ShouldRetrieveUserFromDatabase()
        {
            // Arrange
            var newUser = new User
            {
                IsAdmin = false,
                Address = new Capital
                {
                    Name = "TestCity",
                    Continent = Continent.Asia,
                    Coordinate = new Coordinate(0, 0)
                },
                Username = "TestUser",
                Email = "testuser@example.com"
            };

            _dbContext.AddUser(newUser);

            // Act
            var retrievedUser = _dbContext.GetUserByUsername("TestUser");

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(newUser.Id, retrievedUser.Id);
            Assert.AreEqual(newUser.Username, retrievedUser.Username);
            Assert.AreEqual(newUser.Address, retrievedUser.Address);
            Assert.AreEqual(newUser.Email, retrievedUser.Email);
        }

        // Add similar tests for other methods

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
