namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Status
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public static Status Submitted = new()
        {
            Id = 1,
            Name = nameof(Submitted).ToLowerInvariant(),
        };
        public static Status AwaitingValidation = new()
        {
            Id = 2,
            Name = nameof(AwaitingValidation).ToLowerInvariant(),
        };
        public static Status StockConfirmed = new() { Id = 3, Name = nameof(StockConfirmed).ToLowerInvariant() };
        public static Status Paid = new()
        {
            Id = 4,
            Name = nameof(Paid).ToLowerInvariant(),
        };
        public static Status Shipped = new() { Id = 5, Name = nameof(Shipped).ToLowerInvariant() };
        public static Status Cancelled = new()
        {
            Id = 6,
            Name = nameof(Cancelled).ToLowerInvariant(),
        };
    }
}
