using System.ComponentModel.DataAnnotations;

namespace WebApp.Application.Services.DTOs
{
    public class BasketDTO
    {
        [Required]
        public AddressDTO Address { get; set; }

        [Required]
        public CardDTO Card { get; set; }

        public string Buyer { get; set; }
    }
}
