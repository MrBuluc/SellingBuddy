using EventBus.Base.Events;
using OrderService.Application.DTOs;
using OrderService.Domain.Models;

namespace OrderService.Api.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderNumber { get; set; }
        public AddressDTO Address { get; set; }
        public CardDTO Card { get; set; }
        public string Buyer { get; set; }
        public CustomerBasket Basket { get; set; }
    }
}
