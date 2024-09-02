using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Services;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NotificationService.Infrastructure.MailServices
{
    public class MailService(IOptions<MailSettings> options) : IMailService
    {
        private readonly MailSettings settings = options.Value;

        public async Task<bool> SendFailedMail(string recipient, string customerName, string orderId, string errorMessage) => SendMail(recipient, "Failed Order", await PrepareFailedOrderEmail(customerName, orderId, errorMessage));

        private async Task<string> PrepareFailedOrderEmail(string customerName, string orderId, string errorMessage) => (await File.ReadAllTextAsync(Path.Combine(Environment.CurrentDirectory, "email-templates", "failed-email.html")))
            .Replace("{{Customer Name}}", customerName)
            .Replace("{{OrderId}}", orderId)
            .Replace("{{Error Message}}", errorMessage);

        public bool SendMail(string recipient, string subject, string bodyHtml)
        {
            using SmtpClient smtp = new(settings.SmtpServer, settings.SmtpPort);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(settings.SmtpUsername, settings.SmtpPassword);

            using MailMessage message = new()
            {
                From = new(settings.SenderEmail, settings.SenderName),
                Subject = $"SellingBuddy {subject}",
                Body = bodyHtml,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            message.To.Add(recipient);

            smtp.Send(message);
            return true;
        }

        public async Task<bool> SendSuccessMail(string recipient, string customerName, string orderId) => SendMail(recipient, "Success Order", await PrepareSuccessOrderEmail(customerName, orderId));

        private async Task<string> PrepareSuccessOrderEmail(string customerName, string orderId) => (await File.ReadAllTextAsync(Path.Combine(Environment.CurrentDirectory, "email-templates", "success-email.html")))
            .Replace("{{Customer Name}}", customerName)
            .Replace("{{OrderId}}", orderId);
    }
}
