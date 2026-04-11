using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface ILoggingService
    {
        public void LogInfo(string ip, string message, string? userEmail = null);
        public void LogWarning(string ip, string message, string? userEmail = null);
        public void LogError(string ip, string message, string? userEmail = null);
        public List<string> GetLogs();
    }
}