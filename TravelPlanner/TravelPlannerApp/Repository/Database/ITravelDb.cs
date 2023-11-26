using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.TravelPlannerApp.Repository.Database
{
    internal interface ITravelDb
    {
        void ConnectDatabase();
        void DisconnectDatabase();
        void ExecuteNonQuery(string query);

    }
}
