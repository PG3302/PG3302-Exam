using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Models;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    public class MockUserDatabase : IMockDatabase
    {
        private readonly List<User> _userList = new();
        private int _mockCurrentId = 0;

        //Adding mock data to userList
        public MockUserDatabase()
        {
            AddUser(new("Ole", new("Oslo", new(10, 10), Data.DataType.Continent.Europe)));
            AddUser(new("Dole", new("Washington", new(-30, -30), Data.DataType.Continent.NorthAmerica)));
            AddUser(new("Doffen", new("Cape Town", new(10, -50), Data.DataType.Continent.Africa)));
        }

        public User AddUser(User user)
        {
            ConnectDatabase(user.Username);

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

        public List<User> GetAllUsers()
        {
            return _userList;
        }

        public User? GetUserById(long id)
        {
            User? requestedUser = _userList.Find(i => i.Id == id);

            return requestedUser;
        }

        public void ConnectDatabase(string username)
        {
            Console.WriteLine($"User Database Connected, added ${username}...");
        }

        public void DisconnectDatabase()
        {
            Console.WriteLine("User Database Disconnected...");
        }

        public void ConnectDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
