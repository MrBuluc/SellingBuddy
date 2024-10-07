namespace WebApp.Domain.Models.ViewModels
{
    public class Item
    {
        public string Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
