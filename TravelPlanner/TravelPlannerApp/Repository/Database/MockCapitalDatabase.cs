using TravelPlanner.TravelPlannerApp.Data.Log;
using TravelPlanner.TravelPlannerApp.Data.Objects;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal class MockCapitalDatabase : IMockDatabase
    {
        private readonly List<Capital> _capitalList = new();
        private long _mockCurrentId = 0;

        public MockCapitalDatabase()
        {
            AddCapital(new("Oslo", new(10, 10), Data.DataType.Continent.Europe));
            AddCapital(new("Washington", new(-30, -30), Data.DataType.Continent.NorthAmerica));
            AddCapital(new("Cape Town", new(10, -50), Data.DataType.Continent.Africa));
        }

        public Capital AddCapital(Capital capital)
        {
            ConnectDatabase();

            //Mock
            capital.Id = _mockCurrentId;
            _mockCurrentId++;
            _capitalList.Add(capital);

            Logger.LogInfo($"Capital {capital.Name} added.");

            //Statement
            return (capital);
        }

        public Capital? GetCapitalByName(string name)
        {
            Capital? requestedCapital = _capitalList.Find(n => n.Name == name);

            return requestedCapital;
        }

        public Capital? GetCapitalById(long id)
        {
            Capital? requestedCapital = _capitalList.Find(i => i.Id == id);

            return requestedCapital;
        }

        public void ConnectDatabase()
        {
            Console.WriteLine("Capital Database Connected...");
        }

        public void DisconnectDatabase()
        {
            Console.WriteLine("Capital Database Disconnected...");
        }
    }
}
