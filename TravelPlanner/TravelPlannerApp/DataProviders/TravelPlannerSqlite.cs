using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TravelPlanner.TravelPlannerApp.Repository.Models;
using SQLitePCL; // Import SQLitePCL package???????

namespace TravelPlanner.TravelPlannerApp.DataProviders
{
    public class TravelPlannerSqlite : TravelPlannerProvider //DONT FIX UNTIL MODELS ARE DONE
    {
        private readonly string _dbPath;
        private readonly string _conString;

        public TravelPlannerSqlite(string dbPath)
        {
            _dbPath = dbPath;
            _conString = $"Data Source = {_dbPath}";

        }

        public Destination? CreateDestination(int Id)
        {
            throw new NotImplementedException();
        }

        public void CreateUser()
        {
            int id = -1;
            using SqliteConnection con = new SqliteConnection(_conString);
            con.Open();
        }
        public IList<Destination> getAnything()
        {
            IList<Destination> dest = new List<Destination>();

            using SqliteConnection con = new SqliteConnection(_conString);
            con.Open();
            SqliteCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * FROM City ORDER BY Population;";
            using SqliteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                dest.Add(new() {
                    CityName = reader.GetString(2),
                    Population = reader.GetInt32(4)              
                });
                
            }
            Console.WriteLine(dest.Count);
            return dest;
        }

        public Destination? GetDestination(int Id)
        {
            throw new NotImplementedException();
        }

        public IList<Destination> GetDestination()
        {
            throw new NotImplementedException();
        }




        //Implement methods according to interface when done





    }
}
