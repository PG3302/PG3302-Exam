using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.TravelPlannerApp.DataProviders
{
    public class TravelPlannerSqlite : TravelPlannerProvider
    {
        private readonly string _dbPath;
        private readonly string _conString;

        public TravelPlannerSqlite(string dbPath, string conString)
        {
            _dbPath = dbPath;
            _conString = conString;
        }


        //Implement methods according to interface when done





    }
}
