using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
namespace AnrukiShop_Application.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _repo;

        public LoggingService(ILoggingRepository repo)
        {
            _repo = repo;
        }

        private void WriteLog(string ip, string level, string message, string? userEmail)
        {
            var logLine = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | {level.ToUpper()} | IP:{ip} | {(userEmail == null ? "SYSTEM" : userEmail)} | {message}";
           
            _repo.Log(logLine);
        }

        public void LogInfo(string ip, string message, string? userEmail) => WriteLog(ip, "Info", message, userEmail);
        public void LogWarning(string ip, string message, string? userEmail) => WriteLog(ip, "Warning", message, userEmail);
        public void LogError(string ip, string message, string? userEmail) => WriteLog(ip, "Error", message, userEmail);
        public List<string> GetLogs() => _repo.GetLogs();
    }
}