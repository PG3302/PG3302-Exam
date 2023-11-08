namespace TravelPlanner.TravelPlannerApp
{
    internal class TravelPlanner
    {
        public void Start()
        {
            Console.WriteLine("I am the main program.");
            MainMenu menu = new MainMenu();
            menu.DisplayMenu();
            
        }
    }
}
