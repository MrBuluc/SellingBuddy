using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Application.Services.DTOs
{
    public class CardDTO
    {
        public string Number { get; set; }
        public string HolderName { get; set; }
        public DateTime Expiration { get; set; }
        public string ExpirationShort { get; set; }
        public string SecurityNumber { get; set; }

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
