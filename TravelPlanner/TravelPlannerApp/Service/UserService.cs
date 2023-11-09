using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Objects;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service
{
    public class UserService
    {
        private readonly MockUserDatabase userDatabase = new();

        public User AddUser(string username, Capital capital, bool isAdmin = false)
        {
            User newUser = new(username, capital, isAdmin);

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

        public User? GetUserById(long id)
        {
            return userDatabase.GetUserById(id);
        }
    }
}