using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderPaymentSuccessIntegrationEvent(Guid orderId, string customerName, string customerEmail) : IntegrationEvent
    {
        public Guid OrderId { get; } = orderId;
        public string CustomerName { get; } = customerName;
        public string CustomerEmail { get; } = customerEmail;
    }
}
