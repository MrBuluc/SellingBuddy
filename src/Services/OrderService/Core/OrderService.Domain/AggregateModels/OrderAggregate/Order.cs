using OrderService.Domain.Common;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; private set; }
        public int Quantity { get; private set; }
        public string Description { get; private set; }
        public Address Address { get; private set; }
        private readonly List<Item> items = [];
        public IReadOnlyCollection<Item> Items => items;

        public Guid? BuyerId { get; set; }
        public Buyer Buyer { get; private set; }

        private int statusId;
        public Status Status { get; private set; }

        protected Order() => Id = Guid.NewGuid();

        public Order(string userName, Address address, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration, Guid? buyerId = null) : this()
        {
            BuyerId = buyerId;
            statusId = Status.Submitted.Id;
            Date = DateTime.UtcNow;
            Address = address;

            AddOrderStartedDomainEvent(userName, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        }

        private void AddOrderStartedDomainEvent(string userName, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            AddDomainEvent(new OrderStartedDomainEvent
            {
                UserName = userName,
                Card = new(cardNumber, cardSecurityNumber, cardHolderName, cardExpiration),
                Order = this
            });
        }

        public void AddOrderItem(Product product, int units = 1)
        {
            // orderItem validations

            items.Add(new()
            {
                Product = product,
                Units = units
            });
        }

    }
}
