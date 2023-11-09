using TravelPlanner.TravelPlannerApp.Data.Objects;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal class MockUserDatabase : IMockDatabase
    {
        private long _mockCurrentId = 0;

        public User AddUser(User user)
        {
            ConnectDatabase();

            user.Id = _mockCurrentId;
            _mockCurrentId++;

            //Statement
            return (user);
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
