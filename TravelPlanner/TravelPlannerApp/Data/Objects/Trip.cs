namespace TravelPlanner.TravelPlannerApp.Data.Objects
{
    internal class Trip
    {
        public long Id { get; private set; }
        public User User { get; private set; }
        public Capital StartingLocation { get; private set; }
        public Capital DestinationLocation { get; private set; }
        public int Price { get; private set; }

        public Trip(long id, User user, Capital startingLocation, Capital destinationLocation, int price)
        {
            Id = id;
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
