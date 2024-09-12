namespace BasketService.Application.DTOs
{
    public class CustomerBasketDTO
    {
        public string BuyerId { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
