namespace TravelPlanner.TravelPlannerApp.Controller.MenuControllers
{
    internal class ListMenus : UIController
    {
        internal void ListMenu()
        {
            _menuController.AddMenu("Back.", MainMenu);
            _menuController.AddMenu("Filter", ListMenuFilter);
            _menuController.AddList(_capitalService.GetCapitalAll(), MainMenu);
            _menuController.RunMenu("List of travel locations...", MainMenu);
        }

        private void ListMenuFilter()
        {

        }
    }
}
