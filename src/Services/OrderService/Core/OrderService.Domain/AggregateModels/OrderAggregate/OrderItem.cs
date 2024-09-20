using OrderService.Domain.Common;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public Product Product { get; set; }
        public int Units { get; set; }
    }
}
