using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        public void Start()
        {
            Console.WriteLine("I am the main program.");

            ErrorCreator();
        }

        public void ErrorCreator()
        {
            int[] tooShort = new int[1];
            int longEnough = 0;

            try
            {
                longEnough = tooShort[1];
            } catch(IndexOutOfRangeException error)
            {
                Logger.LogError("Personal message for each exception.", error);
            }
        }
    }
}