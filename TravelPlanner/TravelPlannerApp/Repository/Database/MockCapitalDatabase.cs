using TravelPlanner.TravelPlannerApp.Data.DataType;
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
            AddCapital(new("Oslo", new(59.91666667, 10.75), Continent.Europe));
            AddCapital(new("Washington", new(38.883333, -77), Continent.NorthAmerica));
            AddCapital(new("Cape Town", new(-25.7, 28.216667), Continent.Africa));
            AddCapital(new("Stockholm", new(59.33333333, 18.05), Continent.Europe));
            AddCapital(new("Cairo", new(30.05, 31.25), Continent.Africa));
            AddCapital(new("Wellington", new(-41.3, 174.783333), Continent.Oceania));
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
