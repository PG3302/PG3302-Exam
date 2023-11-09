using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Objects;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal class MockUserDatabase : IMockDatabase
    {
        private List<User> userList = new();
        private long _mockCurrentId = 0;

        public User AddUser(User user)
        {
            ConnectDatabase();

            user.Id = _mockCurrentId;

            //Mock
            _mockCurrentId++;
            userList.Add(user);

            Logger.LogInfo($"User {user.Username} added.");

            //Statement
            return (user);
        }

        public User? GetUserByUsername(string username)
        {
            User? requestedUser = userList.Find(u => u.Username == username);

            return requestedUser;
        }

        public User? GetUserById(long id)
        {
            User? requestedUser = userList.Find(i => i.Id == id);

            return requestedUser;
        }

        public void ConnectDatabase()
        {
            Console.WriteLine("User Database Connected...");
        }

        public void DisconnectDatabase()
        {
            Console.WriteLine("User Database Disconnected...");
        }
    }
}
