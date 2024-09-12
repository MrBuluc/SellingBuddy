namespace BasketService.Application.DTOs
{
    public class BasketItemDTO
    {
        public Guid Id { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}
