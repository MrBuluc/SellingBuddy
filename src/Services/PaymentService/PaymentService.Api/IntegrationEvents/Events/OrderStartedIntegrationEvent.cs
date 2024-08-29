using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent(Guid orderId, string cardNumber) : IntegrationEvent
    {
        public Guid OrderId { get; set; } = orderId;
        public string CardNumber { get; set; } = cardNumber;
    }
}
