using TravelDatabase.Data.Log;

namespace TravelDatabase.Data.Handlers
{
    public static class FileHandler
    {
        private static readonly string _filePath = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments
        );
        private static readonly string _fileName = "TravelPlannerLog.txt";
        private static readonly string _defaultFullPath = Path.Combine(_filePath, _fileName);
        private static readonly object _fileLock = new();

        public static void WriteToFile(string message, string? path = null, bool append = true)
        {
            string filePath = path ?? _defaultFullPath;

            lock (_fileLock)
            {
                using StreamWriter output = new(filePath, append);
                output.WriteLine(message);
            }
        }

        public static string? ReadFromFile(string? path = null)
        {
            string filePath = path ?? _defaultFullPath;

            try
            {
                lock (_fileLock)
                {
                    using StreamReader? content = new(filePath);
                    return content.ReadToEnd();
                }
            }
            catch (IOException error)
            {
                Logger.LogError("Error when reading from file.", error);
                return null;
            }
        }

        public static void DeleteFile(string? path = null)
        {
            string filePath = path ?? _defaultFullPath;

            lock (_fileLock)
            {
                File.Delete(filePath);
            }
        }
    }
}
