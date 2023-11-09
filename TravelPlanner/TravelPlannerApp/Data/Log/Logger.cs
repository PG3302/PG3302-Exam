using TravelPlanner.TravelPlannerApp.Data.Handlers;

namespace TravelPlanner.TravelPlannerApp.Data.Log
{
    //Singleton design pattern from https://csharpindepth.com/Articles/Singleton
    public class Logger
    {
        private static Logger? _instance = null;
        private static readonly object _constructLock = new();

        private Logger()
        {

        }

        public static Logger GetLogger()
        {
            if (_instance == null)
            {
                lock (_constructLock)
                {
                    _instance ??= new();
                }
            }

            return _instance;
        }

        public static void LogError(string message, Exception? exception = null, string? path = null, bool writeToConsole = false)
        {
            string formatedMessage = $"[Error] {exception?.Message} -- {message}";

            WriteToLog(formatedMessage, path, writeToConsole);
        }

        public static void LogInfo(string message, string? path = null, bool writeToConsole = false)
        {
            string formatedMessage = $"[Info] {message}";

            WriteToLog(formatedMessage, path, writeToConsole);
        }

        private static void WriteToLog(string formatedMessage, string? path, bool writeToConsole)
        {
            FileHandler.WriteToFile(formatedMessage, path);
            if (writeToConsole)
                Console.WriteLine(formatedMessage);
        }
    }
}