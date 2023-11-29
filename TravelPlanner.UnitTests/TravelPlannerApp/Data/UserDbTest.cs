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

        [Test]
        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
