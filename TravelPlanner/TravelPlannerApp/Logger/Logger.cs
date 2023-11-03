namespace TravelPlanner.TravelPlannerApp.Logger
{
    //Singleton design pattern from https://csharpindepth.com/Articles/Singleton
    internal class Logger
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

        public static void LogError(string message, Exception? exception = null)
        {
            string formatedMessage = $"[Error]\t{exception?.Message} -- {message}";

            WriteToLog(formatedMessage);
        }

        public static void LogInfo(string message)
        {
            string formatedMessage = $"[Info]\t\t{message}";

            WriteToLog(formatedMessage);
        }

        private static void WriteToLog(string formatedMessage)
        {
            FileHandler.WriteToFile(formatedMessage);
            Console.WriteLine(formatedMessage);
        }
    }
}