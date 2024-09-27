using OrderService.Domain.Common;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public static Status Submitted = new()
        {
            Name = nameof(Submitted).ToLowerInvariant(),
        };
        public static Status AwaitingValidation = new()
        {
            Name = nameof(AwaitingValidation).ToLowerInvariant(),
        };
        public static Status StockConfirmed = new() { Name = nameof(StockConfirmed).ToLowerInvariant() };
        public static Status Paid = new()
        {
            Name = nameof(Paid).ToLowerInvariant(),
        };
        public static Status Shipped = new() { Name = nameof(Shipped).ToLowerInvariant() };
        public static Status Cancelled = new()
        {
            Name = nameof(Cancelled).ToLowerInvariant(),
        };
    }
}
