using MediatR;
using NotificationService.Application.Features.Mail.Command.Send;
using NotificationService.Application.Interfaces.Services;

namespace NotificationService.Application.Features.Mail.Command.SendSuccess
{
    public class SendSuccessMailCommandHandler(IMailService mailService) : IRequestHandler<SendSuccessMailCommandRequest, Unit>
    {
        private readonly IMailService mailService = mailService;

        public async Task<Unit> Handle(SendSuccessMailCommandRequest request, CancellationToken cancellationToken)
        {
            await mailService.SendSuccessMail(request.Recipient, request.CustomerName, request.OrderId.ToString());

            return Unit.Value;
        }
    }
}
