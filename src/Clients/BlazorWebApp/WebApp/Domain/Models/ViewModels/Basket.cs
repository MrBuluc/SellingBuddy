namespace WebApp.Domain.Models.ViewModels
{
    public class Basket
    {
        public List<Item> Items { get; set; } = [];
        public string BuyerId { get; set; }
    }
}
