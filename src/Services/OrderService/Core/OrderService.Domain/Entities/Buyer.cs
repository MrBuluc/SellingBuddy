using OrderService.Domain.Common;
using OrderService.Domain.Events;

namespace OrderService.Domain.Entities
{
    public class Buyer(string name) : BaseEntity
    {
        public string Name { get; set; } = name;
        private List<Card> cards = [];
        public IEnumerable<Card> Cards => cards.AsReadOnly();

        public Card VerifyAndAddCard(string cardNumber, string securityNumber, string cardHolderName, DateTime expiration, Guid orderId)
        {
            Card? existingCard = cards.SingleOrDefault(c => c.IsEqualTo(cardNumber, expiration));

            if (existingCard is not null)
            {
                // raise event
                AddDomainEvent(new BuyerAndCardVerifiedDomainEvent
                {
                    Buyer = this,
                    Card = existingCard,
                    OrderId = orderId,
                });

                return existingCard;
            }

            Card newCard = new(cardNumber, securityNumber, cardHolderName, expiration);

            cards.Add(newCard);

            // raise event
            AddDomainEvent(new BuyerAndCardVerifiedDomainEvent
            {
                Buyer = this,
                Card = newCard,
                OrderId = orderId,
            });

            return newCard;
        }

        public override bool Equals(object? obj) => base.Equals(obj) || obj is Buyer buyer && Id.Equals(buyer.Id) && Name == buyer.Name;
    }
}
