using BasketService.Application.DTOs;

namespace BasketService.Application.Features.CustomerBasket.Command.Checkout
{
    public class CheckoutCustomerBasketCommandResponse
    {
        public CustomerBasketDTO CustomerBasket { get; set; }
    }
}
