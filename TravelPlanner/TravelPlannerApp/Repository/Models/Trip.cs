using TravelDatabase.DataAccess.SqLite;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Data.Models
{
    public class Trip : Model
    {
        public int Id { get; set; }
        public User? User { get;  set; }
        public Capital DepartureId { get;  set; }
        public Capital ArrivalId { get;  set; }
        public int Price { get; set; }

        public Trip(User user, Capital departureId, Capital arrivalId)
        {
            User = user;
            DepartureId = departureId;
            ArrivalId = arrivalId;
        }

        //Maybe this should be somewhere else... but good here for now.
        public static class GeoCalculator
        {
            private const double EarthRadiusKm = 6371.0; // Earth radius in kilometers

            public static double CalculateDistance(Coordinate coordinate1, Coordinate coordinate2)
            {
                double dlat = ToRadians(coordinate2.Latitude - coordinate1.Latitude);
                double dlon = ToRadians(coordinate2.Longitude - coordinate1.Longitude);

                double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(ToRadians(coordinate1.Latitude)) * Math.Cos(ToRadians(coordinate2.Latitude)) * Math.Pow(Math.Sin(dlon / 2), 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                double distance = EarthRadiusKm * c;

                return distance;
            }

            private static double ToRadians(double degrees)
            {
                return degrees * (Math.PI / 180);
            }
        }

        public override string ToString()
        {
            TravelDbContext tbc = new TravelDbContext();
            Capital startingCapital = tbc.GetCapitalById(DepartureId.Id);
            Capital destinationCapital = tbc.GetCapitalById(ArrivalId.Id);
            double distance = GeoCalculator.CalculateDistance(DepartureId.Coordinate, ArrivalId.Coordinate);
            return $"{Id}: {User} -- {startingCapital} -> {destinationCapital} ({distance})";
        }
    }

    

}
