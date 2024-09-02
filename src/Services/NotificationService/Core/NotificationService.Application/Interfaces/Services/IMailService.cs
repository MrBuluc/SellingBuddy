namespace NotificationService.Application.Interfaces.Services
{
    public interface IMailService
    {
        bool SendMail(string recipient, string subject, string bodyHtml);
        Task<bool> SendSuccessMail(string recipient, string customerName, string orderId);
        Task<bool> SendFailedMail(string recipient, string customerName, string orderId, string errorMessage);
    }
}
