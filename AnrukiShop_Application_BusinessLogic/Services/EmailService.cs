using System.Net;
using System.Net.Mail;
using AnrukiShop_Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace AnrukiShop_Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            var smtp = new SmtpClient(_config["Email:Host"])
            {
                Port = int.Parse(_config["Email:Port"]),
                Credentials = new NetworkCredential( _config["Email:Username"], _config["Email:Password"]),
                EnableSsl = true
            };

            var mail = new MailMessage(
                _config["Email:From"],
                toEmail,
                subject,
                body
            );

            mail.IsBodyHtml = true;

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}