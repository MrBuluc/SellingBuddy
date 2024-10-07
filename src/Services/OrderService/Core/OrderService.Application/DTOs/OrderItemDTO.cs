namespace OrderService.Application.DTOs
{
    public class OrderItemDTO
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}
