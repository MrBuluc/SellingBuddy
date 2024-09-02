using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent(Guid orderId, string cardNumber, string customerName, string customerEmail) : IntegrationEvent
    {
        public Guid OrderId { get; set; } = orderId;
        public string CardNumber { get; set; } = cardNumber;
        public string CustomerName { get; } = customerName;
        public string CustomerEmail { get; } = customerEmail;
    }
}
