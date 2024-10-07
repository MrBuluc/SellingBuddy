using EventBus.Base.Events;
using PaymentService.Api.DTOs;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent(Guid orderId, CardDTO card, string customerName, string customerEmail) : IntegrationEvent
    {
        public Guid OrderId { get; set; } = orderId;
        public CardDTO Card { get; set; } = card;
        public string CustomerName { get; } = customerName;
        public string CustomerEmail { get; } = customerEmail;
    }
}
