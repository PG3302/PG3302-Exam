using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.TravelPlannerApp.Repository.Models
{
    public  class User
    {
        public int Id { get; set; } = int.MinValue;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //public IList<TravelPath> UserPaths { get; set; } = new List<TravelPath>(); //????? not real pls fix but need to make it list of users "traveled paths", or travel paths in general? failed paths? idk just something
        public override string ToString()
        {
            return $"ID:${Id}\nUser name: ${Name}|Email: ${Email}";
        }
    }
}
