using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface ILoggingRepository
    {
        public void Log(string logLine);
        public List<string> GetLogs();
    }
}