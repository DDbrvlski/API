using System.Net;
using System.Net.Mail;

namespace BookStoreAPI.Services.Email
{
    public class EmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public void SendEmail(string to, string subject, string body)
        {
            // Konfiguracja ustawień SMTP
            var smtpClient = ConfigureGmailSmtpClient();

            // Tworzenie wiadomości e-mail
            var message = CreateNewMessage(to, subject, body);

            // Wysłanie wiadomości
            smtpClient.Send(message);
        }

        private SmtpClient ConfigureGmailSmtpClient()
        {
            return new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password),
                EnableSsl = true,
            };
        }

        private MailMessage CreateNewMessage(string to, string subject, string body)
        {
            return new MailMessage(_emailConfiguration.Email, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
        }
    }
}
