namespace TravelDatabase.Repositories {
	internal interface ITravelDb {
		void ConnectDatabase();
		void DisconnectDatabase();
		void ExecuteNonQuery(string query);

	}
}
