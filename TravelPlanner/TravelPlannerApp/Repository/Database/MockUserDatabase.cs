using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Objects;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    public class MockUserDatabase : IMockDatabase
    {
        private readonly List<User> _userList = new();
        private long _mockCurrentId = 0;

        //Adding mock data to userList
        public MockUserDatabase()
        {
            AddUser(new("Ole", new("Oslo", new(10, 10), Data.DataType.Continent.Europe)));
            AddUser(new("Dole", new("Washington", new(-30, -30), Data.DataType.Continent.NorthAmerica)));
            AddUser(new("Doffen", new("Cape Town", new(10, -50), Data.DataType.Continent.Africa)));
        }

        public User AddUser(User user)
        {
            ConnectDatabase();

            //Mock
            user.Id = _mockCurrentId;
            _mockCurrentId++;
            _userList.Add(user);

            Logger.LogInfo($"User {user.Username} added.");

            //Statement
            return (user);
        }

        public User? GetUserByUsername(string username)
        {
            User? requestedUser = _userList.Find(u => u.Username == username);

            return requestedUser;
        }

        public User? GetUserById(long id)
        {
            User? requestedUser = _userList.Find(i => i.Id == id);

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
