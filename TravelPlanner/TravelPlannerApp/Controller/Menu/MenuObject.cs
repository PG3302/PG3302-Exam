namespace TravelPlanner.TravelPlannerApp.Controller.Menu
{
    internal class MenuObject
    {
        //Fix later
        public Func<int, int> Method { get; private set; }
        //public int Index { get; set; }
        public string Text { get; set; }

        public MenuObject(string text)
        {
            //this.Index = index;
            this.Text = text;
        }
    }
}
