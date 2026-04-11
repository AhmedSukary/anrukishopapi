namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendAsync(string toEmail, string subject, string body);
    }
}