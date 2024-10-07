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
            ValidCardNumberResult isPaymentSuccess = CardService.IsValidCardNumber(@event.Card);

            logger.LogInformation($"OrderStartedIntegrationEventHandler in PaymentService is fired with PaymentSuccess: {isPaymentSuccess}, orderId: {@event.OrderId}");

            eventBus.Publish(isPaymentSuccess.IsValid ? new OrderPaymentSuccessIntegrationEvent(@event.OrderId, @event.CustomerName, @event.CustomerEmail) : new OrderPaymentFailedIntegrationEvent(@event.OrderId, isPaymentSuccess.ErrorMessage!, @event.CustomerName, @event.CustomerEmail));

            return Task.CompletedTask;
        }
    }
}
