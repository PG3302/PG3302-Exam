using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Data.DataType;


namespace TravelDatabase.DataAccess.SqLite
{
    /* TODO:
     * GetAllTrips() 
     * GetAllCapitals()
     * What else?
    */
    public class TravelDbContext : DbContext
    {

        private readonly String _conString = @"data Source = Resources\Travel.db";


        public DbSet<User> User => Set<User>();
        public DbSet<Trip> Trip => Set<Trip>();
        public DbSet<Capital> Capital => Set<Capital>();
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"data Source = Resources\Travel.db");
        }*/


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

            using SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT Id AS TripID, ArrivalID, DepartureID, UserID
                FROM Trip
                WHERE Id = $Id;
            ";

            cmd.Parameters.AddWithValue("$Id", tripId);

            using SqliteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                trip = new Trip
                (
                    arrivalId: GetCapitalById(reader.GetInt32(1)),
                    departureId: GetCapitalById(reader.GetInt32(2)),
                    user: GetSingleUser(reader.GetInt32(0))
                );
            }

            con.Close();
            return trip;
        }

        public User? GetSingleUser(int userId)
        {
            User? user = null;
            if (userId <= 0)
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
            user.Username = reader.GetString(2);
            user.Address = GetCapitalById(reader.GetInt32(3));
            user.Email = reader.GetString(4);


            //Add more when database finnished

            return user;
        }

        public List<User> GetUsers()
        {

            List<User> userList = new List<User>();
            using SqliteConnection con = new(_conString);
            con.Open();

            SqliteCommand cmd = con.CreateCommand();
            //going to be changed with userDatabase Setup
            cmd.CommandText = @"SELECT Id, Name, CityId FROM User";
            cmd.CommandText += ";";

            using SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User tempUser = new User(
                    username: reader.GetString(1),
                    address: GetCapitalById(reader.GetInt32(2)),
                    isAdmin: false, // Example value, replace with actual value
                    email: reader.GetString(3)
                );

                userList.Add(tempUser);
            }

            return userList;
        }

        public Capital GetCapitalById(long capitalId)
        {
            Capital? capital = null;
            if (capitalId <= 0)
            {
                return capital;
            }
            using SqliteConnection con = new(_conString);
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Capital";
            cmd.CommandText += @"WHERE Id = $Id;";

            cmd.Parameters.AddWithValue($"Id", capitalId);
            using SqliteDataReader reader = cmd.ExecuteReader();
            capital.Id = reader.GetInt32(0);
            capital.Name = reader.GetString(1);
            capital.Continent = Enum.Parse<Continent>(reader.GetString(2));
            return capital;
        }
    }
}