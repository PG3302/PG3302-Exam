using System.Configuration;
using TravelDatabase.Data.Log;
using TravelPlanner.TravelPlannerApp.Controller.ConsoleControllers;

namespace TravelPlanner.TravelPlannerApp.Controller.UserControllers
{
    internal class UserController
    {
        private readonly ConsoleController _consoleController = new();

        int minNameLength;

        internal UserController()
        {
            SetConfigValues();
        }

        private void SetConfigValues()
        {
            try
            {
                string? minNameLengthValue = ConfigurationManager.AppSettings["minNameLength"];
                minNameLength = int.Parse(minNameLengthValue ?? "3");

                if (minNameLength < 1)
                {
                    Logger.LogError("Illegal value for MinNameLength", new ArgumentNullException());
                }
            }
            catch (Exception error)
            {
                Logger.LogError("Error when reading app.config", error);
            }
        }

        internal string GetUserString(bool allowEmpty = false)
        {
            string userInput = "";

            _consoleController.ShowCursor();
            while (userInput.Length < minNameLength)
            {
                userInput = Console.ReadLine() ?? "";

                if (allowEmpty && userInput.Length == 0)
                {
                    break;
                }
                else if (userInput.Length < minNameLength!)
                {
                    Console.Write(
                        $"Value too small. Min value {minNameLength}. Please provide a valid value: "
                    );
                }
            }

            _consoleController.HideCursor();

            return userInput;
        }

        internal int GetUserInt()
        {
            _consoleController.ShowCursor();

            //https://stackoverflow.com/questions/45030/how-to-parse-a-string-into-a-nullable-int
            _ = int.TryParse(Console.ReadLine(), out int intValue) ? intValue : 0;

            _consoleController.HideCursor();

            return intValue;
        }

        internal int GetUserIntMinMax(int minValue, int maxValue)
        {
            int value = 0;
            bool validValue = false;

            _consoleController.ShowCursor();

            while (!validValue)
            {
                value = GetUserInt();

                if (value >= minValue && value <= maxValue)
                {
                    validValue = true;
                }
                else
                {
                    Console.Write(
                        $"Please type in a valid number between {minValue} - {maxValue}: "
                    );
                }
            }

            _consoleController.HideCursor();

            return value;
        }

        internal ConsoleKey GetUserMenuChoiceKey(List<ConsoleKey> arrowKeysAllowed)
        {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            bool validValue = false;

            arrowKeysAllowed.Add(ConsoleKey.Escape);
            arrowKeysAllowed.Add(ConsoleKey.Enter);

            while (!validValue)
            {
                foreach (ConsoleKey key in arrowKeysAllowed)
                {
                    if (keyPressed == key)
                    {
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
