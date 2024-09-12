namespace BasketService.Application.DTOs
{
    public class CardDTO
    {
        public string Number { get; set; }
        public string HolderName { get; set; }
        public DateTime Expiration { get; set; }
        public string SecurityNumber { get; set; }
    }
}
