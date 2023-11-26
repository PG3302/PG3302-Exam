using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Data.Models
{
    public class User : Model
    {
        public long Id { get; set; }
        public string Username { get;  set; }
        public Capital Address { get;  set; }
        public bool IsAdmin { get; set; }
        public string Email { get;  set; }

        public User(string username, Capital address, bool isAdmin = false, string email = "")
        {
            Username = username;
            Address = address;
            IsAdmin = isAdmin;
            Email = email;  
        }

        public override string ToString()
        {
            return $"{Id}: {Username} from {Address.Name}";
        }
    }
}
