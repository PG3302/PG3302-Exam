using TravelDatabase.Models;
using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service {
	public class UserService
    {
        private readonly MockUserDatabase userDatabase = new();

        public UserModel AddUser(string username, CapitalModel capital, bool isAdmin = false)
        {
            UserModel newUser = new(username, capital, isAdmin);

            return userDatabase.AddUser(newUser);
        }

        public UserModel? GetUserByUsername(string username)
        {
            UserModel? requestedUser = userDatabase.GetUserByUsername(username);

            //Security for finding brute force attacks
            if (requestedUser == null) 
                Logger.LogInfo($"Request for username {username} not found.");

            return requestedUser;
        }

        public UserModel? GetUserById(long id)
        {
            return userDatabase.GetUserById(id);
        }
    }
}