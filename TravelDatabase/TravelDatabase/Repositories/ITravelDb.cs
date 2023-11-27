namespace TravelPlanner.TravelPlannerApp.Repository.Database {
	internal interface ITravelDb
    {
        void ConnectDatabase();
        void DisconnectDatabase();
        void ExecuteNonQuery(string query);

    }
}
