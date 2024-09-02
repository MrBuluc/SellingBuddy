using MediatR;

namespace NotificationService.Application.Features.Mail.Command.SendFailed
{
    public class SendFailedMailCommandRequest : IRequest<Unit>
    {
        public string Recipient { get; set; }
        public string CustomerName { get; set; }
        public Guid OrderId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
