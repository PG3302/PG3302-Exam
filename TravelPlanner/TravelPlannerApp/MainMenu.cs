namespace TravelPlanner.TravelPlannerApp
{
    public class MainMenu
    {
        public void DisplayMenu()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("Travelplanner hehe xd:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Search");
                Console.WriteLine("3. Exit");

                if (int.TryParse(Console.ReadLine(), out int selectedOption))
                {
                    if (selectedOption == 1)
                        ProcessOption("Option 1. Login Page"); // Login?
                    else if (selectedOption == 2)
                        ProcessOption("Option 2 Search Page"); //  Søkemotor?
                    else if (selectedOption == 3)
                    {
                        Console.WriteLine("Exiting program");
                        break;
                    }
                    else
                        Console.WriteLine("Invalid Option");

                }

                else
                    Console.WriteLine("Invalid Option.");

                Console.WriteLine("Press Enter to continue..");
                Console.ReadLine();
            }
        }

        private static void ProcessOption(string message)
        {
            Console.WriteLine(message);

            switch (message)
            {
                case "1. Login":
                    Console.WriteLine("Her kommer login siden");
                    break;
                case "2. Search":
                    Console.WriteLine("Her kommer søkefunksjon? el/annet");
                    break;
                default:
                    Console.WriteLine("Hmm, noe funky");
                    break;
            }
        }
    }
}