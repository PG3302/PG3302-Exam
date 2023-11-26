using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelDatabase.DataAccess.SqLite;
using TravelPlanner.TravelPlannerApp.Data.DataType;
using TravelPlanner.TravelPlannerApp.Data.Models;
using TravelPlanner.TravelPlannerApp.Repository.Database;

namespace TravelPlanner.TravelPlannerApp.Repository
{
    internal class CapitalDatabase : ITravelDb
    {
        private readonly TravelDbContext _db;

        public CapitalDatabase()
        {

        }

        public Capital? GetCapitalByName(string username)
        {
            return _db.GetCapitalByName(username);
        }

        public Capital? GetCapitalById(long id)
        {
            return _db.GetCapitalById(id);
        }

        public Capital? AddCapital(Capital newCapital)
        {
            _db.AddCapital(newCapital);
            return newCapital;
        }
        public Capital? AddCapital(long Id, string name, Coordinate coord, Continent cont)
        {
            Capital newCapital = new Capital(name, coord, cont);
            try
            {
                _db.AddCapital(newCapital);
                return newCapital;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the user: {ex.Message}");
                return null;
            }
        }

        public List<Capital> GetAllCapitals()
        {
            return _db.GetAllCapitals();
        }




        //do i need this here?
        public void ConnectDatabase()
        {
            _db.Database.OpenConnection();
        }

        public void DisconnectDatabase()
        {
            _db.Database.CloseConnection();
        }

        public void ExecuteNonQuery(string query)
        {
            _db.Database.ExecuteSqlRaw(query);
        }
    }
}
