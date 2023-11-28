using Microsoft.EntityFrameworkCore;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;
using TravelDatabase.Models;

namespace TravelDatabase.Repositories
{
    public class UserRepository 
		{
		public UserModel AddUser(UserModel newUser) {
			using TravelDbContext travelDbContext = new();
			if (!travelDbContext.User.Any(u => u.Email == newUser.Email)) //if input email does not exist in DB list w user emails 
				{
				User user = new();
				{
					user.Name = $"{newUser}";
					user.Email = newUser.Email ;
					if (newUser.IsAdmin == false) {
						user.Admin = 0;
					} else {
						user.Admin = 1;}
				}
				Logger.LogInfo($"Adding user to DB: {user}"); 
				travelDbContext.User.Add(user);
				travelDbContext.SaveChanges();
				return MapUser(user);
			}
			throw new Exception("Admin value not allowed/Email exists in db");
		}

		//Only Admins should get access to this
		public List<UserModel?> GetUserAll() 
		{
			using TravelDbContext travelDbContext = new();
			Logger.LogInfo("Getting all users to List of User objects");
			List<User> users = travelDbContext.User.ToList();
			return users.Select(u => MapUser(u)).ToList();
		}

		public UserModel? GetUserByEmail(string email)
        {
			using TravelDbContext travelDbContext = new();
			Logger.LogInfo($"Fetching user by email: {email}");
			User? user = travelDbContext.User.FirstOrDefault(u => u.Email == email);
			Logger.LogInfo($"Found {user}, returning...");
			return MapUser(user);
        }

		public UserModel? GetUserById(int id)
        {
			using TravelDbContext travelDbContext = new();
            Logger.LogInfo($"Fetching user by email: {id}");
            User? user = travelDbContext.User.Find(id);
            Logger.LogInfo($"Found {user}, returning...");
            return MapUser(user);
        }
        
        public void DeleteUser(int userId) 
		{
			using TravelDbContext travelDbContext = new();
            Logger.LogInfo($"Attempting to delete user with userId: {userId}");
            User user = travelDbContext.User.First(user => user.Id == user.Id);
            Logger.LogInfo($"Found {user}. Attempting to delete...");
            travelDbContext.User.Remove(user);
            Logger.LogInfo($"Deleting user: {user}");
            travelDbContext.SaveChanges();
		}
		public void EditUser(int userId , string Name , int Admin)
		{
			using TravelDbContext travelDbContext = new();
            Logger.LogInfo($"Attempting to edit a user based on parameters: Id: {userId}, Name: {Name}, and ifAdmin: {Admin} (0 == False, 1 == True)");
            User oldUser = travelDbContext.User.First(user => user.Id == userId);
			Logger.LogInfo($"Found user: {oldUser}");
			oldUser.Name = Name;
			oldUser.Admin = Admin;
			Logger.LogInfo($"New user info: Name: {Name}, isAdmin: {Admin}");
			travelDbContext.SaveChanges();
		}

		internal static UserModel? MapUser(User? user) {
			if (user == null) {
				Logger.LogError($"Failed to map {user} to UserModel");
				return null;
			}
			Logger.LogInfo($"Mapping user: {user} to UserModel");
			return new UserModel(
				user.Id,
				user.Name! , 
				user.Email!, 
				user.Admin == 1);
		}
	}
}