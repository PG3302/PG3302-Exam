using TravelDatabase.Data.Log;
using TravelDatabase.Models;
using TravelDatabase.Repositories;

// Database saves here: \\TravelDatabase\TravelDatabase\bin\Debug\net6.0\Resources

namespace TravelDatabase {
	public class Program {
		static void Main() {
			InitDatabase.Init();
		}
	}
}
