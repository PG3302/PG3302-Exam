using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using Microsoft.Extensions.Options;


namespace TravelDatabase.DataAccess.SqLite
{
    /* TODO:
     * What else?
    */
    public class TravelDbContext : DbContext
    {
        private readonly String _conString = @"Data Source = Resources\TravelDB.db";

        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {
            
        }
        public TravelDbContext(){}

        public DbSet<User> User => Set<User>();
        public DbSet<Trip> Trip => Set<Trip>();
        public DbSet<Capital> Capital => Set<Capital>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Resources\\TravelDB.db");
        }


        // USER FUNCTIONS
        /*
        public User? GetUserById(int userId)
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
            user.Address = GetCapitalById(reader.GetInt32(2));
            user.Username = reader.GetString(3);

            user.Email = reader.GetString(4);


            //Add more when database finnished

            return user;
        }
        public User? GetUserByUsername(string username)
        {
            User? user = null;
            if (username == null)
            {
                return user;
            }
            using SqliteConnection con = new(_conString);
            con.Open();

            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM User";
            cmd.CommandText += @"WHERE Name = $Name;";
            cmd.Parameters.AddWithValue($"Name", username);

            using SqliteDataReader reader = cmd.ExecuteReader();

            user.Id = reader.GetInt32(0);
            user.Address = GetCapitalById(reader.GetInt32(2));
            user.Username = reader.GetString(3);
            user.Email = reader.GetString(4);

            return user;
        }
        public User? AddUser(User newUser)
        {
            string query = "INSERT INTO Users (IsAdmin, Adress, Name, Email) " +
                       "VALUES (@IsAdmin, @Adress, @Name, @Email)";

            using SqliteConnection con = new(_conString);
            con.Open();


            using (var command = con.CreateCommand())
            {
                command.CommandText = query;
                //command.Parameters.AddWithValue("@Id", newUser.Id); Should be auto incr.
                command.Parameters.AddWithValue("@IsAdmin", newUser.IsAdmin);
                command.Parameters.AddWithValue("@CityId", newUser.Address);
                command.Parameters.AddWithValue("@Name", newUser.Username);
                command.Parameters.AddWithValue("@Email", newUser.Email);

                command.ExecuteNonQuery();
            }
            con.Close();
            return newUser;
        }
        public List<User> GetAllUsers()
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
       
        */

        public User? GetUserByUsername(string username)
        {
            return User.FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserById(int id)
        {
            return User.Find(id);
        }

        public User? AddUser(User newUser)
        {
            User.Add(newUser);
            SaveChanges();
            return newUser;
        }
        public User? AddUser(string username, Capital capital, string email, bool isAdmin = false)
        {
            User newUser = new User(username, capital, isAdmin, email);

            try
            {
                // Add the new user to the User DbSet
                User.Add(newUser);

                // Save changes to the database
                SaveChanges();

                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the user: {ex.Message}");
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            return User.ToList();
        }


        //CITY / CAPITAL FUNCTIONS
        /*
        public Capital? GetCapitalById(int capitalId)
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
            
            double capitalLat = reader.GetDouble(3);
            double capitalLong = reader.GetDouble(4);

            Coordinate coord = new Coordinate(capitalLong, capitalLat);
            capital.Coordinate = coord;
            return capital;
        }
        public Capital? GetCapitalByName(string capitalName)
        {
            Capital? capital = null;
            if (capitalName == null)
            {
                return capital;
            }
            using SqliteConnection con = new(_conString);
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Capital";
            cmd.CommandText += @"WHERE Name = $Name;";

            cmd.Parameters.AddWithValue($"Name", capitalName);
            using SqliteDataReader reader = cmd.ExecuteReader();
            capital.Id = reader.GetInt32(0);
            capital.Name = reader.GetString(1);
            capital.Continent = Enum.Parse<Continent>(reader.GetString(2));

            double capitalLat = reader.GetDouble(3);
            double capitalLong = reader.GetDouble(4);

            Coordinate coord = new Coordinate(capitalLong, capitalLat);
            capital.Coordinate = coord;

            return capital;
        }
        public Capital? AddCapital(Capital? capital)
        {
            if (capital == null)
            {
                return null;
            }

            string query = "INSERT INTO Capital (Name, Continent, Latitude, Longitude) " +
                           "VALUES (@Name, @Continent, @Latitude, @Longitude)";

            using SqliteConnection con = new(_conString);
            con.Open();


            using (var command = con.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@Name", capital.Name);
                command.Parameters.AddWithValue("@Continent", capital.Continent.ToString());
                command.Parameters.AddWithValue("@Latitude", capital.LatCoordToString(capital.Coordinate));
                command.Parameters.AddWithValue("@Longitude", capital.LongCoordToString(capital.Coordinate));

                command.ExecuteNonQuery();
            }

            con.Close();
            return capital;
        }
        */


        public Capital? GetCapitalById(int capitalId)
        {
            if (capitalId <= 0)
            {
                return null;
            }
            return Capital
                .Where(c => c.Id == capitalId)
                .Include(c => c.Coordinate)
                .FirstOrDefault();
        }

        public Capital? GetCapitalByName(string capitalName)
        {
            if (capitalName == null)
            {
                return null;
            }

            return Capital
                .Where(c => c.Name == capitalName)
                .Include(c => c.Coordinate)
                .FirstOrDefault();
        }

        public Capital? AddCapital(Capital? capital)
        {
            if (capital == null)
            {
                return null;
            }
            Capital.Add(capital);
            SaveChanges();

            return capital;
        }

        public List<Capital> GetAllCapitals()
        {
            return Capital.ToList();
        }


        //TRIP FUNCTIONS

        /*
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
                    destinationCapital: GetCapitalById(reader.GetInt32(1)),
                    startingCapital: GetCapitalById(reader.GetInt32(2)),
                    user: GetUserById(reader.GetInt32(4))
                );
            }

            con.Close();
            return trip;
        }
        public Trip? GetTripByUserId(int userId) 
        {
            Trip trip = null;
            return trip;
        }
        public List<Trip?> GetAllTrips()
        {
            List<Trip?> tripList = new List<Trip?>();
            return tripList;
        }
        */

        public Trip? GetSingleTrip(int tripId)
        {
            if (tripId <= 0)
            {
                Console.WriteLine("Invalid input for trip data request");
                return null;
            }

            // Use EF Core to fetch Trip by Id
            return Trip
                .Where(t => t.Id == tripId)
                .Include(t => t.DestinationCapital)
                    .ThenInclude(c => c.Coordinate) // Assuming Coordinate is a navigation property
                .Include(t => t.StartingCapital)
                    .ThenInclude(c => c.Coordinate) // Assuming Coordinate is a navigation property
                .Include(t => t.User)
                .FirstOrDefault();
        }

        public Trip? GetTripByUserId(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            // Use EF Core to fetch Trip by UserId
            return Trip
                .Where(t => t.User.Id == userId)
                .Include(t => t.DestinationCapital)
                    .ThenInclude(c => c.Coordinate) // Assuming Coordinate is a navigation property
                .Include(t => t.StartingCapital)
                    .ThenInclude(c => c.Coordinate) // Assuming Coordinate is a navigation property
                .Include(t => t.User)
                .FirstOrDefault();
        }

        public List<Trip> GetAllTrips()
        {
            // Use EF Core to fetch all Trips
            return Trip
                .Include(t => t.DestinationCapital)
                    .ThenInclude(c => c.Coordinate) // Assuming Coordinate is a navigation property
                .Include(t => t.StartingCapital)
                    .ThenInclude(c => c.Coordinate) // Assuming Coordinate is a navigation property
                .Include(t => t.User)
                .ToList();
        }

    }
}