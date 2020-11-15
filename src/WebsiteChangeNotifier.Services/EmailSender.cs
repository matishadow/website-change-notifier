using System.Net;
using System.Net.Mail;
using WebsiteChangeNotifier.Data;
using WebsiteChangeNotifier.Interfaces;

namespace WebsiteChangeNotifier.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig _emailConfig;
        private readonly SmtpClient _smtpClient;

        public EmailSender(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;

            _smtpClient = new SmtpClient(emailConfig.SmtpHost)
            {
                Port = 587,
                Credentials = new NetworkCredential(emailConfig.Login, emailConfig.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };
        }

        public void SendEmail(string subject, string content)
        {
            _smtpClient.Send(_emailConfig.Login, _emailConfig.Receiver, subject, content);
        }
    }
}