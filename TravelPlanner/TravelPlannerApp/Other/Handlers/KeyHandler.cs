using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.TravelPlannerApp.Other.Handlers
{
    internal static class KeyHandler
    {
        public static void Test()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.UpArrow)
            {
                Console.WriteLine("YAYAYA");
            }

            Console.WriteLine(Console.ReadKey(true).KeyChar.ToString());
            Console.ReadLine();
        }
    }
}
