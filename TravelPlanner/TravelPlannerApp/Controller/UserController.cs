namespace TravelPlanner.TravelPlannerApp.Controller
{
    public class UserController
    {
        public string GetUserString()
        {
            return Console.ReadLine() ?? "";
        }

        public int GetUserInt()
        {
            //https://stackoverflow.com/questions/45030/how-to-parse-a-string-into-a-nullable-int
            _ = int.TryParse(Console.ReadLine(), out int intValue) ? intValue : 0;

            return intValue;
        }

        public int GetUserMenuChoiceInt(int minValue, int maxValue)
        {
            int value = 0;
            bool validValue = false;

            while (!validValue)
            {
                value = GetUserInt();

                if (value >= minValue && value <= maxValue)
                {
                    validValue = true;
                }
                else
                {
                    Console.Write($"Please type in a valid number between {minValue} - {maxValue}: ");
                }
            }

            return value;
        }

        public ConsoleKey GetUserMenuChoiceKey(List<ConsoleKey> arrowKeysAllowed)
        {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            bool validValue = false;

            arrowKeysAllowed.Add(ConsoleKey.Escape);
            arrowKeysAllowed.Add(ConsoleKey.Enter);

            while (!validValue)
            {
                foreach(ConsoleKey key in arrowKeysAllowed)
                {
                    if (keyPressed == key)
                    {
                        Console.WriteLine("uigrhuirgu");
                        validValue = true;
                        break;
                    }
                }

                if (!validValue) 
                    keyPressed = Console.ReadKey(true).Key;
            }

            return keyPressed;
        }
    }
}
