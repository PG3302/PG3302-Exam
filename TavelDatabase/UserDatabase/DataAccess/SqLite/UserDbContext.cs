using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDatabase.Entities;

namespace UserDatabase.DataAccess.SqLite {
	public class UserDbContext : DbContext {
		public DbSet<User> User => Set<User>();
		public DbSet<Trip> Trip => Set<Trip>();
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlite(@"data Source = Resources\User.db");
		}

	}
}
