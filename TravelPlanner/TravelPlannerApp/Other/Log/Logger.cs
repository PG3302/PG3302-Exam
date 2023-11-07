using TravelPlanner.TravelPlannerApp.Other.Handlers;

namespace TravelPlanner.TravelPlannerApp.Other.Log
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

        public static void LogError(string message, Exception? exception = null, string? path = null, bool append = true)
        {
            string formatedMessage = $"[Error]\t{exception?.Message} -- {message}";

            WriteToLog(formatedMessage, path, append);
        }

        public static void LogInfo(string message, string? path = null, bool append = true)
        {
            string formatedMessage = $"[Info]\t\t{message}";

            WriteToLog(formatedMessage, path, append);
        }

        private static void WriteToLog(string formatedMessage, string? path = null, bool append = true)
        {
            FileHandler.WriteToFile(formatedMessage, path, append);
            Console.WriteLine(formatedMessage);
        }
    }
}