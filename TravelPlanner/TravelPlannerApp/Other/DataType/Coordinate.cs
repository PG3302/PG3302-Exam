namespace TravelPlanner.TravelPlannerApp.Other.DataType
{
    public class Coordinate
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        //Find distance between two coordinates
        public static double operator -(Coordinate coordinate1, Coordinate coordinate2)
        {
            double longitudePower = Math.Pow(coordinate1.Longitude - coordinate2.Longitude, 2);
            double latitudePower = Math.Pow(coordinate1.Latitude - coordinate2.Latitude, 2);

            return Math.Sqrt(longitudePower + latitudePower);
        }

        public override string ToString()
        {
            return $"Longitude: {Longitude}, Latitude: {Latitude}";
        }
    }
}
