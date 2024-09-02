using EventBus.Base.Abstraction;
using MediatR;
using NotificationService.Application.Features.Mail.Command.SendFailed;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using Serilog;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    public class OrderPaymentFailedIntegrationEventHandler(IMediator mediator) : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly IMediator mediator = mediator;

        public async Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {
            await mediator.Send(new SendFailedMailCommandRequest
            {
                Recipient = @event.CustomerEmail,
                CustomerName = @event.CustomerName,
                OrderId = @event.OrderId,
                ErrorMessage = @event.ErrorMessage,
            });

            Log.Logger.Information($"Order Payment failed with OrderId: {@event.OrderId}, ErrorMessage: {@event.ErrorMessage}");
        }
    }
}
