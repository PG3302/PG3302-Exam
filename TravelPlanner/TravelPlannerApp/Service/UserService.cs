using TravelDatabase.DataAccess.SqLite;
using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service
{
    public class UserService
    {
        private readonly TravelDbContext userDatabase = new();

        public User? AddUser(string username, Capital capital, bool isAdmin = false, string email = "")
        {
            User newUser = new(username, capital, isAdmin, email);

            return userDatabase.AddUser(newUser);
        }

        public User? GetUserByUsername(string username)
        {
            User? requestedUser = userDatabase.GetUserByUsername(username);


            //Security for finding brute force attacks
            if (requestedUser == null) 
                Logger.LogInfo($"Request for username {username} not found.");

            return requestedUser;
        }

        public User? GetUserById(int id)
        {
            return userDatabase.GetUserById(id);
        }

        public List<User> GetAllUsers() {
            return userDatabase.GetAllUsers();
        }

    }
}