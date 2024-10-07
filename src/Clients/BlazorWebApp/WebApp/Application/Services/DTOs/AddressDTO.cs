using System.ComponentModel.DataAnnotations;

namespace WebApp.Application.Services.DTOs
{
    public class AddressDTO
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
