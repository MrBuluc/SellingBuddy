using OrderService.Domain.AggregateModels.OrderAggregate;

namespace OrderService.Domain.Models
{
    public class CustomerBasket
    {
        public string BuyerId { get; set; }
        public List<Item> Items { get; set; } = [];
    }
}
