using OrderService.Domain.Common;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; private set; }
        public int Quantity { get; private set; }
        public Address Address { get; private set; }
        private readonly List<Item> items = [];
        public IReadOnlyCollection<Item> Items => items;

        public Guid? BuyerId { get; set; }
        public Buyer Buyer { get; private set; }

        private int statusId;
        public Status Status { get; private set; }

        protected Order() => Id = Guid.NewGuid();

        public Order(string userName, Address address, Card card, int submittedStatusId, Guid? buyerId = null) : this()
        {
            BuyerId = buyerId;
            statusId = submittedStatusId;
            Date = DateTime.UtcNow;
            Address = address;

            AddOrderStartedDomainEvent(userName, card.Number, card.SecurityNumber, card.HolderName, card.Expiration);
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
                Quantity = units
            });

            Quantity += units;
        }

    }
}
