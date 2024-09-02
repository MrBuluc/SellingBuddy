
using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderPaymentFailedIntegrationEvent(Guid orderId, string errorMessage, string customerName, string customerEmail) : IntegrationEvent
    {
        public Guid OrderId { get; } = orderId;
        public string ErrorMessage { get; } = errorMessage;
        public string CustomerName { get; } = customerName;
        public string CustomerEmail { get; } = customerEmail;
    }
}
