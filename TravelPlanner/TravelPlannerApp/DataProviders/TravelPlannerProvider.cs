using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.TravelPlannerApp.Repository.Models;

namespace TravelPlanner.TravelPlannerApp
{
    public interface TravelPlannerProvider
    {
        Destination? GetDestination(int Id);
        Destination? CreateDestination(int Id); //Fill with create data as we go along

    }
}
