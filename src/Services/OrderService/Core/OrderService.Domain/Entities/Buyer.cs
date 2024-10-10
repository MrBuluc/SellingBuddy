using OrderService.Domain.Common;
using OrderService.Domain.Events;

namespace OrderService.Domain.Entities
{
    public class Buyer(string name) : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public ICollection<Card> Cards { get; set; } = [];

        public Card VerifyOrAddCard(string cardNumber, string securityNumber, string cardHolderName, DateTime expiration, Guid orderId)
        {
            // First, check if the card already exists in the tracked entities

            Card? existingCard = Cards.SingleOrDefault(c => c.IsEqualTo(cardNumber, expiration));

            if (existingCard is not null)
            {
                // raise event
                AddDomainEvent(new BuyerAndCardVerifiedDomainEvent
                {
                    BuyerId = Id,
                    OrderId = orderId,
                });

                return existingCard;
            }

            Card newCard = new(cardNumber, securityNumber, cardHolderName, expiration, Id);

            Cards.Add(newCard);

            // raise event
            AddDomainEvent(new BuyerAndCardVerifiedDomainEvent
            {
                BuyerId = Id,
                OrderId = orderId,
            });

            return newCard;
        }

        public override bool Equals(object? obj) => base.Equals(obj) || obj is Buyer buyer && Id.Equals(buyer.Id) && Name == buyer.Name;
    }
}
