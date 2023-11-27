using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.Data.DataType.DataAccess.SqLite;
using TravelDatabase.Data.Log;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories
{
    public class UserRepository 
		{
		public static int AddUser(string username , Capital city , int admin) {
			if (admin == 0 || admin == 1) 
				{
				User user = new();
				{
					user.Name = $"{username}";
					user.CityId = city.Id; //needs city selected from Capital table
					user.Admin = admin;
				}
				using TravelDbContext travelDbContext = new();
				Logger.LogInfo($"Adding user to DB: {user}"); 
				travelDbContext.User.Add(user);
				travelDbContext.SaveChanges();

				return user.Id;
			}
			throw new Exception("Admin value not allowed");
		}

		//Only Admins should get access to this
		public List<User> GetUserAll(int userId) 
		{
			using TravelDbContext travelDbContext = new();
			Logger.LogInfo("Admin is fetching all users...");
			return travelDbContext.User.ToList();
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
		public void EditUser(int userId , string Name , int CityId, int Admin)
		{
			using TravelDbContext travelDbContext = new();
			User oldUser = travelDbContext.User.First(user => user.Id == userId);
			oldUser.Name = Name;
			oldUser.CityId = CityId;
			oldUser.Admin = Admin;
			travelDbContext.SaveChanges();
		}

	}
}
