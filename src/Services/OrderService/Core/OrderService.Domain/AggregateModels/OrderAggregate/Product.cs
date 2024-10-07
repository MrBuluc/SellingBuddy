namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
    }
}
