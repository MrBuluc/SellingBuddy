using EventBus.Base.Events;
using BasketService.Application.DTOs;

namespace BasketService.Api.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public User User { get; set; }
        public Guid OrderNumber { get; set; }
        public AddressDTO Address { get; set; }
        public CardDTO Card { get; set; }
        public string Buyer { get; set; }
        public CustomerBasketDTO Basket { get; set; }
    }
}
