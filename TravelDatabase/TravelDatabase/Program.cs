using TravelDatabase.Repositories;

namespace TravelDatabase {
	public class Program {
		static void Main() {
			InitDatabase.InitFromCsv();
		}
	}
}
