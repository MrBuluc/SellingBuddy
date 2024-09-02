using EventBus.Base.Abstraction;
using MediatR;
using NotificationService.Application.Features.Mail.Command.Send;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using Serilog;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSuccessIntegrationEventHandler(IMediator mediator) : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        private readonly IMediator mediator = mediator;

        public async Task Handle(OrderPaymentSuccessIntegrationEvent @event)
        {
            await mediator.Send(new SendSuccessMailCommandRequest
            {
                Recipient = @event.CustomerEmail,
                OrderId = @event.OrderId,
                CustomerName = @event.CustomerName,
            });

            Log.Logger.Information($"Order Payment Success with OrderId: {@event.OrderId}");
        }
    }
}
