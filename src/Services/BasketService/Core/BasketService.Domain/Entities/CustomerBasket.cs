using BasketService.Domain.Common;

namespace BasketService.Domain.Entities
{
    public class CustomerBasket() : EntityBase<Guid>(Guid.NewGuid())
    {
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = [];
    }
}
