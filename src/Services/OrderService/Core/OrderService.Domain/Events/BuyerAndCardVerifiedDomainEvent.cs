using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Events
{
    public class BuyerAndCardVerifiedDomainEvent : INotification
    {
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
    }
}
