namespace TravelPlanner.TravelPlannerApp.Data.Objects
{
    internal class Trip
    {
        public long Id { get; private set; }
        public User User { get; private set; }
        public Location StartingLocation { get; private set; }
        public Location DestinationLocation { get; private set; }
        public int Price { get; private set; }

        public Trip(User user, Location startingLocation, Location destinationLocation, int price)
        {
            User = user;
            StartingLocation = startingLocation;
            DestinationLocation = destinationLocation;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Id}: {User} -- {StartingLocation} -> {DestinationLocation} ({Price})";
        }
    }
}
