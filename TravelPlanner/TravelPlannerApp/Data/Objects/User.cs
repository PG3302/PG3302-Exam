namespace TravelPlanner.TravelPlannerApp.Data.Objects
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public Location Address { get; private set; }

        public User(string username, Location address)
        {
            Username = username;
            Address = address;
        }

        public override string ToString()
        {
            return $"{Id}: {Username} from {Address}";
        }
    }
}
