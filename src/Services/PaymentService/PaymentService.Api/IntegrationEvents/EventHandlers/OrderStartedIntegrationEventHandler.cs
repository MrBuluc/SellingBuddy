using EventBus.Base.Abstraction;
using PaymentService.Api.IntegrationEvents.Events;
using PaymentService.Api.Services;

namespace PaymentService.Api.IntegrationEvents.EventHandlers
{
    public class OrderStartedIntegrationEventHandler(IEventBus eventBus, ILogger<OrderStartedIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IEventBus eventBus = eventBus;
        private readonly ILogger<OrderStartedIntegrationEventHandler> logger = logger;

        public Task Handle(OrderStartedIntegrationEvent @event)
        {
            bool isPaymentSuccess = CardService.IsValidCardNumber(@event.CardNumber);

            logger.LogInformation($"OrderStartedIntegrationEventHandler in PaymentService is fired with PaymentSuccess: {isPaymentSuccess}, orderId: {@event.OrderId}");

            eventBus.Publish(isPaymentSuccess ? new OrderPaymentSuccessIntegrationEvent(@event.OrderId) : new OrderPaymentFailedIntegrationEvent(@event.OrderId, "Card number is invalid"));

            return Task.CompletedTask;
        }
    }
}
