using EventBus.Base.Events;
using OrderService.Application.DTOs;

namespace OrderService.Application.IntegrationEvents
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public CardDTO Card { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public Guid OrderId { get; set; }
    }
}
