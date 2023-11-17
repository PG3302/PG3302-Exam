using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.Entities;
using Microsoft.Data.Sqlite;
using TravelDatabase.Entities;


namespace TravelDatabase.DataAccess.SqLite
{
    public class TravelDbContext : DbContext
    {

        private readonly String _conString = @"data Source = Resources\Travel.db";


        public DbSet<User> User => Set<User>();
        public DbSet<Trip> Trip => Set<Trip>();
        public DbSet<Capital> Capital => Set<Capital>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"data Source = Resources\Travel.db");
        }


        public Trip? GetSingleTrip(int tripId)
        {
            Trip? trip = null;

            if (tripId <= 0)
            {
                Console.WriteLine("Invalid input for trip data request");
                return trip;
            }
            using SqliteConnection con = new(_conString);
            con.Open();

            //Add so we get all attributes of Trip object

            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Trip";
            cmd.CommandText += @"WHERE Id = $Id;";


            cmd.Parameters.AddWithValue("$Id", tripId);

            using SqliteDataReader reader = cmd.ExecuteReader();
            trip.Id = reader.GetInt32(0);
            trip.UserId = reader.GetInt32(1);
            //trip.Arrival = reader.GetString(2);
            //trip.Arrival = reader.GetString(3);

            con.Close();
            return trip; //Edit so it returns correct values for each instance
        }
        public User? GetSingleUser(int userId)
        {
            User? user = null;
            if(userId <= 0)
            {
                return user;
            }
            using SqliteConnection con = new(_conString);
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM User";
            cmd.CommandText += @"WHERE Id = $Id;";

            cmd.Parameters.AddWithValue($"Id", userId);

            using SqliteDataReader reader = cmd.ExecuteReader();
            user.Id = reader.GetInt32(0);
            user.Name = reader.GetString(1);
            //Add more when database finnished

            return user;
        }

        public List<User> GetUsers(int? userId = null)
        {

            List<User> userList = null;
            using SqliteConnection con = new(_conString);
            con.Open();

            SqliteCommand cmd = con.CreateCommand();
            //going to be changed with userDatabase Setup
            cmd.CommandText = @"SELECT Id, Name, CityId FROM User";
            if(userId != null)
            {
                cmd.CommandText += @"WHERE Id = $Id";
                cmd.Parameters.AddWithValue("Id", userId);
            }
            cmd.CommandText += ";";

            using SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userList.Add(new() { 
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    //City = reader.GetBoolean(2), City needs a ToString for this to work.
                });
            }
            return userList;
        }
    }
}

