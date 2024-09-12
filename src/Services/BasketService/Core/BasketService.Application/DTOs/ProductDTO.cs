namespace BasketService.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureUrl { get; set; }
    }
}
