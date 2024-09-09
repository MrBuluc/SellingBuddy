namespace WebApiGateway.Application.Models.Basket
{
    public class BasketData
    {
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = [];
    }
}
