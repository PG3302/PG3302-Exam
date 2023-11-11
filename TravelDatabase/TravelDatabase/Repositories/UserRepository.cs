﻿using System;
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
				using TravelDbContext db = new();
				db.User.Add(user);
				db.SaveChanges();

				return user.Id;
			}
			throw new Exception("Admin value not allowed");
		}

	}
}