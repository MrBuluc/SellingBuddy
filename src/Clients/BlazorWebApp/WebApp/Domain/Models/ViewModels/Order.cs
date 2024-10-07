using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Models.ViewModels
{
    public class Order
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        //[RegularExpression(@"^\d{4}-\d{4}-\d{4}-\d{4}$", ErrorMessage = "Number should match xxxx-xxxx-xxxx-xxxx where each x is a digit")]
        [CreditCard(ErrorMessage = "Invalid number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Card Holder name is required")]
        public string HolderName { get; set; }

        public DateTime Expiration { get; set; }

        [Required(ErrorMessage = "Card Expiration date is required")]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string ExpirationShort { get; set; }

        [Required(ErrorMessage = "Card Security number is required")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Security Number should match xxx or xxxx where each x is a digit")]
        public string SecurityNumber { get; set; }

        public string Buyer { get; set; }
        public List<Item> Items { get; } = [];

        public void ExpirationApiFormat()
        {
            if (!string.IsNullOrWhiteSpace(ExpirationShort) && ExpirationShort.Contains('/'))
            {
                string[] monthAndYear = ExpirationShort.Split('/');
                Expiration = new DateTime(int.Parse($"20{monthAndYear[1]}"), int.Parse(monthAndYear[0]), 1);
            }
        }
    }
}
