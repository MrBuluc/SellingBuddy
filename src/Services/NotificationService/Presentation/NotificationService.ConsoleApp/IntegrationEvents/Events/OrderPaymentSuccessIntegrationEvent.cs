using EventBus.Base.Events;

namespace NotificationService.ConsoleApp.IntegrationEvents.Events
{
    public class OrderPaymentSuccessIntegrationEvent(Guid orderId, string customerEmail, string customerName) : IntegrationEvent
    {
        public Guid OrderId { get; } = orderId;
        public string CustomerEmail { get; } = customerEmail;
        public string CustomerName { get; } = customerName;
    }
}
