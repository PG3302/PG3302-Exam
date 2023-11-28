using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TravelDatabase.Data.DataType;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Entities;
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
            _testDatabasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.db");

            var options = new DbContextOptionsBuilder<TravelDbContext>()
                .UseSqlite($"Data Source={_testDatabasePath}")
                .Options;

            _dbContext = new TravelDbContext(options);

            _dbContext.Database.EnsureCreated();
        }
        /*
        [Test]
        public void AddUser_ShouldAddUserToDatabase()
        {
            Capital testCapital = new Capital("TestCapital", new Coordinate(0.0, 0.0), Continent.Asia);
			User newUser = _dbContext.AddUser("TestUser" , testCapital , isAdmin: false , email: "test@example.com");

            _dbContext.AddUser(newUser);

            User addedUser = _dbContext.User.FirstOrDefault(u => u.Name == "TestUser");
            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Name, addedUser.Name);
            Assert.AreEqual(newUser.Email, addedUser.Email);
            Assert.AreEqual(newUser.Email, addedUser.Email);
        }

        [Test]
        public void GetUserById_ShouldRetrieveUserFromDatabase()
        {
            User newUser = new User
            {
                Name = "TestUser",
                IsAdmin = false,                
                Email = "testuser@example.com"
            };

            _dbContext.AddUser(newUser);

            // Act
            User retrievedUser = _dbContext.GetUserByName(newUser.Id);

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(newUser.Id, retrievedUser.Id);
            Assert.AreEqual(newUser.Name, retrievedUser.Name);
            Assert.AreEqual(newUser.Email, retrievedUser.Email);
        }

        // Add similar tests for other methods
        */
        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
        
    }
}
