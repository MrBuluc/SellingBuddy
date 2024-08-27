using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public string CardNumber { get; set; }

        public OrderStartedIntegrationEvent(Guid orderId, string cardNumber)
        {
            OrderId = orderId;
            CardNumber = cardNumber;
        }
    }
}
