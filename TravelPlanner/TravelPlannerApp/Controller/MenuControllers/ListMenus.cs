using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.TravelPlannerApp.Service;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class ListMenus : UIController
    {
        internal void ListMenu()
        {
            _menuController.AddMenu("Back.", MainMenu);
            //_menuController.AddMenu("Filter", )
            _menuController.AddList(_capitalService.GetCapitalAll(), MainMenu);
            _menuController.RunMenu("List of travel locations...", MainMenu);
        }

        private void ListMenuFilter()
        {

        }
    }
}
