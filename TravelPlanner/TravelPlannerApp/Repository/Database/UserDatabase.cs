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
using TravelDatabase.DataAccess.SqLite;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal class CapitalDatabase : ITravelDb
    {
        public CapitalDatabase() { 

        }
        
        private readonly TravelDbContext _db;

        public User? GetUserByUsername(string username)
        {
            return _db.GetUserByUsername(username);
        }

        public User? GetUserById(long id)
        {
            return _db.GetUserById(id);
        }

        public User? AddUser(User newUser)
        {
            _db.AddUser(newUser);
            return newUser;
        }
        public User? AddUser(string username, Capital capital, string email, bool isAdmin = false)
        {
            User newUser = new User(username, capital, isAdmin, email);
            try
            {
                _db.AddUser(newUser);
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
            return _db.GetAllUsers();
        }

        public void ConnectDatabase()
        {
            _db.Database.OpenConnection();
        }

        public void DisconnectDatabase()
        {
            _db.Database.CloseConnection();
        }

        public void ExecuteNonQuery(string query)
        {
            _db.Database.ExecuteSqlRaw(query);
        }
    }
}
