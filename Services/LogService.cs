namespace ConciliadorApp.Services
{
    public class LogService
    {
        private readonly string _logPath = "log.txt";

        public void Log(string message)
        {
            var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(_logPath, line + Environment.NewLine);
        }
    }
}