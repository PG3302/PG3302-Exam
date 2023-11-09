namespace TravelPlanner.TravelPlannerApp.Data.Objects
{
    internal class Trip
    {
        public long Id { get; set; }
        public User User { get; private set; }
        public Capital StartingCapital { get; private set; }
        public Capital DestinationCapital { get; private set; }
        public int Price { get; set; }

        public Trip(User user, Capital startingCapital, Capital destinationCapital)
        {
            User = user;
            StartingCapital = startingCapital;
            DestinationCapital = destinationCapital;
        }

        public override string ToString()
        {
            return $"{Id}: {User} -- {StartingCapital} -> {DestinationCapital} ({Price})";
        }
    }
}
