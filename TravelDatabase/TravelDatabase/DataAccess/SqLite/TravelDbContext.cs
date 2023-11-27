﻿using Microsoft.EntityFrameworkCore;
using TravelDatabase.Entities;


namespace TravelDatabase.DataAccess.SqLite {
	public class TravelDbContext : DbContext
    {
        public DbSet<User> User => Set<User>();
        public DbSet<Trip> Trip => Set<Trip>();
        public DbSet<Capital> Capital => Set<Capital>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"data Source = Resources\Travel.db");
        }
    }
}

