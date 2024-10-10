using OrderService.Domain.Common;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Entities
{
    public class Card(string number, string securityNumber, string holderName, DateTime expiration, Guid buyerId) : BaseEntity
    {
        public string Number { get; set; } = !string.IsNullOrWhiteSpace(number) ? number : throw new OrderingDomainException(nameof(number));
        public string SecurityNumber { get; set; } = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderingDomainException(nameof(securityNumber));
        public string HolderName { get; set; } = !string.IsNullOrWhiteSpace(holderName) ? holderName : throw new OrderingDomainException(nameof(holderName));
        public DateTime Expiration { get; set; } = expiration >= DateTime.UtcNow ? expiration : throw new OrderingDomainException(nameof(expiration));

        public Guid BuyerId { get; set; } = buyerId;
        public Buyer Buyer { get; set; }

        public bool IsEqualTo(string number, DateTime expiration) => Number == number && Expiration == expiration;
    }
}
