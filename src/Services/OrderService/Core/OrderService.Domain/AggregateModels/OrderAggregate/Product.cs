namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
