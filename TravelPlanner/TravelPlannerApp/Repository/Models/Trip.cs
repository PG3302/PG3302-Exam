using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Data.Models
{
    public class Trip : Model
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Capital StartingCapital { get; set; }
        public Capital DestinationCapital { get; set; }
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
