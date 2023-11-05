namespace TravelPlanner.TravelPlannerApp.Object
{
    internal class Coordinate
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Coordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        //Find distance between two coordinates
        public static double operator -(Coordinate coordinate1, Coordinate coordinate2)
        {
            double shortX = Math.Pow(coordinate1.X - coordinate2.X, 2);
            double shortY = Math.Pow(coordinate1.Y - coordinate2.Y, 2);

            return Math.Sqrt(shortX + shortY);
        }

        public override string ToString()
        {
             return $"X: {X}, Y: {Y}";
        }
    }
}
