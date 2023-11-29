using Microsoft.EntityFrameworkCore;
using TravelDatabase.Entities;

namespace TravelDatabase.Data.DataType.DataAccess.SqLite
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext() { }

        public TravelDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> User => Set<User>();
        public DbSet<Trip> Trip => Set<Trip>();
        public DbSet<Capital> Capital => Set<Capital>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Resources\Travel.db");
        }
    }
}
