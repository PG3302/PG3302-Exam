using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;

namespace TravelDatabase.Repositories {
	public class UserRepository {
		public static int AddUser(string username , Capital city , int admin) {
			if (admin == 0 || admin == 1) {
				User user = new();
				{
					user.Name = $"{username}";
					user.CityId = city.Id; //needs city selected from Capital table
					user.Admin = admin;
				}
				using TravelDbContext travelDbContext = new();
				travelDbContext.User.Add(user);
				travelDbContext.SaveChanges();

				return user.Id;
			}
			throw new Exception("Admin value not allowed");
		}

		//Only Admins should get access to this
		public List<User> GetAllUserUsers(int userId) {
			using var travelDbContext = new TravelDbContext();
			return travelDbContext.User.ToList();
		}
		public void DeleteUser(int userId) {
			using var travelDbContext = new TravelDbContext();
			var user = travelDbContext.User.First(user => user.Id == user.Id);
			travelDbContext.User.Remove(user);
			travelDbContext.SaveChanges();
		}
		public void EditUser(int userId , string Name , int CityId, int Admin) {
			using var travelDbContext = new TravelDbContext();
			var oldUser = travelDbContext.User.First(user => user.Id == userId);
			oldUser.Name = Name;
			oldUser.CityId = CityId;
			oldUser.Admin = Admin;
			travelDbContext.SaveChanges();
		}

		public List<User> GetUsers(int? userId = null)
        {

			using var travelDbContext = new TravelDatabase();
            List<User> userList = null;
            using SqliteConnection con = new(_conString);
            con.Open();

            SqliteCommand cmd = con.CreateCommand();
            //going to be changed with userDatabase Setup
            cmd.CommandText = @"SELECT Id, Name, CityId FROM User";
            if(userId != null)
            {
                cmd.CommandText += @"WHERE Id = $Id";
                cmd.Parameters.AddWithValue("Id", userId);
            }
            cmd.CommandText += ";";

            using SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userList.Add(new() { 
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    //City = reader.GetBoolean(2), City needs a ToString for this to work.
                });
            }
            return userList;
        }
		public User? GetSingleUser(int userId)
        {
			using var travelDbContext = new TravelDatabase();
			User? user = null;
            if(userId <= 0)
            {
                return user;
            }
            using SqliteConnection con = new(_conString);
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT * FROM User";
            cmd.CommandText += @"WHERE Id = $Id;";

            cmd.Parameters.AddWithValue($"Id", userId);

            using SqliteDataReader reader = cmd.ExecuteReader();
            user.Id = reader.GetInt32(0);
            user.Name = reader.GetString(1);
            //Add more when database finnished

            return user;
        }
	}
}
