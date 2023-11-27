using Microsoft.EntityFrameworkCore;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;
using TravelDatabase.Models;

namespace TravelDatabase.Repositories
{
    public class UserRepository 
		{
		public static int AddUser(UserModel newUser) {
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

				return user.Id;
			}
			throw new Exception("Admin value not allowed");
		}

		//Only Admins should get access to this
		public List<UserModel?> GetAllUsers(int userId) 
		{
			using TravelDbContext travelDbContext = new();
			List<User> users = travelDbContext.User.ToList();
			return users.Select(u => MapUser(u)).ToList();
		}
        public User? GetUserById(long id)
        {
            using TravelDbContext travelDbContext = new();
            return travelDbContext.User.Find(id);
        }
        public User? GetUserByUsername(string username)
        {
            using TravelDbContext travelDbContext = new();
            return travelDbContext.User.FirstOrDefault(u => u.Name == username);
        }

        public void DeleteUser(int userId) 
		{
			using TravelDbContext travelDbContext = new();
			User user = travelDbContext.User.First(user => user.Id == user.Id);
            Logger.LogInfo("Deleting user: " + user.Id + ", Name: " + user.Name);
            travelDbContext.User.Remove(user);
			travelDbContext.SaveChanges();
		}
		public void EditUser(int userId , string Name , int Admin)
		{
			using TravelDbContext travelDbContext = new();
			User oldUser = travelDbContext.User.First(user => user.Id == userId);
			oldUser.Name = Name;
			oldUser.Admin = Admin;
			travelDbContext.SaveChanges();
		}

		public UserModel? GetUserById(int id)
        {
			using TravelDbContext travelDbContext = new();
			User? user = travelDbContext.User.Find(id);
			return MapUser(user);
        }
        
		public UserModel? GetUserByEmail(string email)
        {
			using TravelDbContext travelDbContext = new();
			User? user = travelDbContext.User.FirstOrDefault(u => u.Email == email);
			return MapUser(user);
        }

		private UserModel? MapUser(User? user) {
			if (user == null) {
				return null;
			}
			return new UserModel(user.Name! , user.Email!, user.Admin == 1);
		}
	}
}
