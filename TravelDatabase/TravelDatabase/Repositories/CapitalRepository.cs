using TravelDatabase.Data.DataType;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;
using TravelDatabase.Models;

namespace TravelDatabase.Repositories
{
    public class CapitalRepository
    {
        private static readonly object _lock = new object();

        public CapitalModel AddCapital(CapitalModel newCapital)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Logger.LogInfo($"Preparing {newCapital} for submitting to database");
                if (travelDbContext.Capital.Any(c => c.CapitalName != newCapital.Name))
                {
                    Capital capital = new();
                    {
                        capital.CapitalName = newCapital.Name;
                        capital.Continent = newCapital.Continent;
                        capital.Longitude = (decimal)newCapital.Coordinate.Longitude;
                        capital.Latitude = (decimal)newCapital.Coordinate.Latitude;
                    }
                    Logger.LogInfo($"Adding capital: {capital}");
                    travelDbContext.Capital.Add(capital);
                    travelDbContext.SaveChanges();
                    Logger.LogInfo($"Capital ({capital}) added");
                    return MapCapital(capital);
                }
                throw new Exception("Capital with this name already exists");
            }
        }

        public List<CapitalModel> GetCapitalAll()
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                List<Capital> capitals = travelDbContext.Capital.ToList();
                Logger.LogInfo("Fetching all capitals from Db to a List with Capital objects...");
                return capitals.Select(c => MapCapital(c)).ToList();
            }
        }

        public CapitalModel? GetCapitalById(int capitalId)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Logger.LogInfo($"Getting capital by {capitalId}");
                Capital? capital = travelDbContext.Capital.Find(capitalId);
                Logger.LogInfo($"Returning {capital}");
                return MapCapital(capital);
            }
        }

        public CapitalModel GetCapitalByName(string name)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Logger.LogInfo($"Getting capital by Name: {name}");
                Capital? capital = travelDbContext
                    .Capital
                    .First(capital => capital.CapitalName == name);
                Logger.LogInfo($"Found {capital}");
                return MapCapital(capital);
            }
        }

        public List<CapitalModel> GetCapitalByContinent(Continent continent)
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new TravelDbContext();
                Logger.LogInfo($"Getting capitals by {continent}");
                List<Capital> capitals = travelDbContext
                    .Capital
                    .Where(c => c.Continent == continent)
                    .ToList();
                Logger.LogInfo($"Found list of capitals based on continent: {continent}");
                return capitals.Select(c => MapCapital(c)).ToList();
            }
        }

        public void EditCapital(
            int capitalId,
            string name,
            Continent continent,
            decimal longitude,
            decimal latitude
        )
        {
            lock (_lock)
            {
                using TravelDbContext travelDbContext = new();
                Capital oldCapital = travelDbContext
                    .Capital
                    .First(capital => capital.Id == capitalId);
                Logger.LogInfo($"Editing capital: {oldCapital}");
                oldCapital.CapitalName = name;
                oldCapital.Continent = continent;
                oldCapital.Longitude = longitude;
                oldCapital.Latitude = latitude;
                Logger.LogInfo($"Saving edited capital as: {oldCapital}");
                travelDbContext.SaveChanges();
            }
        }

        internal static CapitalModel? MapCapital(Capital capital)
        {
            if (capital == null)
            {
                Logger.LogError(
                    "Attempted to map a capital. " + capital.ToString() + ". Input == NULL"
                );
                return null;
            }
            return new CapitalModel(
                capital.Id,
                capital.CapitalName,
                capital.Longitude,
                capital.Latitude,
                capital.Continent
            );
        }
    }
}
