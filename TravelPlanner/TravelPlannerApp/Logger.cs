namespace TravelPlanner.TravelPlannerApp
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
            Console.WriteLine($"[Error]\t{exception?.Message} -- {message}");
        }

        public static void LogInfo(string message)
        {
            Console.WriteLine($"[Info]\t{message}");
        }
    }
}