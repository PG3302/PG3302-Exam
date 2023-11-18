﻿using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp.Data.Models
{
    public class User : Model
    {
        public long Id { get; set; }
        public string Username { get; private set; }
        public Capital Address { get; private set; }
        public bool IsAdmin { get; set; }

        public User(string username, Capital address, bool isAdmin = false)
        {
            Username = username;
            Address = address;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"{Id}: {Username} from {Address.Name}";
        }
    }
}