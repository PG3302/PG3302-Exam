namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    public interface IMockDatabase
    {
        public void ConnectDatabase();
        public void DisconnectDatabase();
    }
}
