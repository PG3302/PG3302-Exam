using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using TravelDatabase.Data.DataType;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;
using TravelDatabase.Models;

namespace TravelDatabase.Repositories
{
    public class InitDatabase
    {
        public static void Init()
        {
            using (TravelDbContext db = new())
            {
                if (db.Capital.Any())
                {
                    Logger.LogInfo("Database already populated. Cancelling Initialization");
                    return;
                }
            }
            InitFromCsv();
            AddTestUsers();
            AddTestTrips();
        }

        public static void InitFromCsv()
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            using (StreamReader reader = new StreamReader(@"Resources\CountryCapitals.csv"))
            using (CsvReader csv = new CsvReader(reader, config))
            {
                IEnumerable<Capital> records = csv.GetRecords<CsvRow>()
                    .Select(
                        row =>
                            new Capital()
                            {
                                CapitalName = row.CapitalName,
                                Continent = MakeStringEnum(row.ContinentName),
                                Longitude = row.CapitalLongitude ?? 0, //substitutes with 0 if value is NULL
                                Latitude = row.CapitalLatitude ?? 0,
                            }
                    );

                using (TravelDbContext db = new())
                {
                    List<Capital> items = records.ToList();

                    db.Capital.AddRange(items);
                    db.SaveChanges();

                    Console.WriteLine(DateTime.Now);
                }
            }
        }

        private static Continent MakeStringEnum(string? str)
        {
            switch (str)
            {
                case "Africa":
                    return Continent.Africa;
                case "Antarctica":
                    return Continent.Antarctica;
                case "Asia":
                    return Continent.Asia;
                case "Europe":
                    return Continent.Europe;
                case "Australia":
                    return Continent.Oceania;
                case "North America":
                    return Continent.NorthAmerica;
                case "South America":
                    return Continent.SouthAmerica;
                case "Central America":
                    return Continent.CentralAmerica;
                default:
                    throw new Exception("No Continent matched");
                //can be shortened w parse enum to string w no space for N /S / C America
            }
        }

        private static void AddTestUsers()
        {
            UserRepository userRepo = new UserRepository();
            userRepo.AddUser(new UserModel(null, "TestUser", "testuser@Test.com", false));
            userRepo.AddUser(new UserModel(null, "TestAdmin", "testadmin@Test.com", true));
        }

        private static void AddTestTrips()
        {
            TripRepository tripRepo = new TripRepository();
            UserRepository userRepo = new UserRepository();
            CapitalRepository capitalRepo = new CapitalRepository();
            List<UserModel> users = userRepo.GetUserAll();
            UserModel user = users.First();
            List<CapitalModel> cities = capitalRepo.GetCapitalAll();
            CapitalModel city = cities.First();
            CapitalModel destination = cities.Last();
            tripRepo.AddTrip(new TripModel(null, user, city, destination));
        }
    }
}
