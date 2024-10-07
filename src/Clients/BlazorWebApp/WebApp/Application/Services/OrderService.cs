using WebApp.Application.Services.DTOs;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models.ViewModels;

namespace WebApp.Application.Services
{
    public class OrderService : IOrderService
    {
        public BasketDTO MapOrderToBasket(Order order)
        {
            order.ExpirationApiFormat();

            return new()
            {
                Address = new()
                {
                    Street = order.Street,
                    City = order.City,
                    State = order.State,
                    Country = order.Country,
                    ZipCode = order.ZipCode,
                },
                Card = new()
                {
                    Number = order.Number,
                    HolderName = order.HolderName,
                    Expiration = order.Expiration,
                    SecurityNumber = order.SecurityNumber,
                },
                Buyer = order.Buyer,
            };
        }
    }
}
