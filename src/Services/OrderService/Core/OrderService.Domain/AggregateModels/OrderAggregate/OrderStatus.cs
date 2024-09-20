namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderStatus
    {
        public string Name { get; private set; }
        public int Id { get; private set; }

        public static OrderStatus Submitted = new()
        {
            Id = 1,
            Name = nameof(Submitted).ToLowerInvariant(),
        };
        public static OrderStatus AwaitingValidation = new()
        {
            Id = 2,
            Name = nameof(AwaitingValidation).ToLowerInvariant(),
        };
        public static OrderStatus StockConfirmed = new() { Id = 3, Name = nameof(StockConfirmed).ToLowerInvariant() };
        public static OrderStatus Paid = new()
        {
            Id = 4,
            Name = nameof(Paid).ToLowerInvariant(),
        };
        public static OrderStatus Shipped = new() { Id = 5, Name = nameof(Shipped).ToLowerInvariant() };
        public static OrderStatus Cancelled = new()
        {
            Id = 6,
            Name = nameof(Cancelled).ToLowerInvariant(),
        };
    }
}
