using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Events
{
    public class BuyerAndCardVerifiedDomainEvent : INotification
    {
        public Buyer Buyer { get; set; }
        public Card Card { get; set; }
        public Guid OrderId { get; set; }
    }
}
