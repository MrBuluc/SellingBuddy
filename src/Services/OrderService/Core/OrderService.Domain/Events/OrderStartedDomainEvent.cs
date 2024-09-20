using MediatR;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string UserName { get; set; }
        public Card Card { get; set; }
        public Order Order { get; set; }
    }
}
