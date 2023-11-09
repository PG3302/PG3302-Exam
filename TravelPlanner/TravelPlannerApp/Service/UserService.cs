using TravelPlanner.TravelPlannerApp.Data.Objects;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Service
{
    internal class UserService
    {
        MockUserDatabase userDatabase = new();

        public User AddUser(string username, Capital capital, bool isAdmin = false)
        {
            User newUser = new(username, capital, isAdmin);

            return userDatabase.AddUser(newUser);
        }
    }
}
