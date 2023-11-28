using TravelDatabase.Models;

namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers.Menus
{
    internal class ListMenus : UIController
    {
        private void ListMenu(List<Model>? list = null)
        {
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.AddMenu("Filter.", ListMenuFilterCapital);

            _menuController.AddList(list ?? _capitalService.GetCapitalAll(), MainMenu);

            _menuController.RunMenu("List of travel locations...", MainMenu);
        }

        private void ListMenuFilterCapital()
        {
            _menuController.AddMenu("")
        }
    }
}
