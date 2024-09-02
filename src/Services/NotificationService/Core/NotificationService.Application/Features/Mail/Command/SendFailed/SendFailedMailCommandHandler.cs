using MediatR;
using NotificationService.Application.Interfaces.Services;

namespace NotificationService.Application.Features.Mail.Command.SendFailed
{
    public class SendFailedMailCommandHandler(IMailService mailService) : IRequestHandler<SendFailedMailCommandRequest, Unit>
    {
        private readonly IMailService mailService = mailService;

        public async Task<Unit> Handle(SendFailedMailCommandRequest request, CancellationToken cancellationToken)
        {
            await mailService.SendFailedMail(request.Recipient, request.CustomerName, request.OrderId.ToString(), request.ErrorMessage);

            return Unit.Value;
        }
    }
}
