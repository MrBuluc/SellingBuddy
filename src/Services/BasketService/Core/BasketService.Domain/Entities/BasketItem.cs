namespace BasketService.Domain.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
