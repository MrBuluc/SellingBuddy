using MediatR;

namespace NotificationService.Application.Features.Mail.Command.Send
{
    public class SendSuccessMailCommandRequest : IRequest<Unit>
    {
        public string Recipient { get; set; }
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
    }
}
