using BasketService.Application.DTOs;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.Checkout
{
    public class CheckoutCustomerBasketCommandRequest : IRequest<CheckoutCustomerBasketCommandResponse>
    {
        public AddressDTO Address { get; set; }
        public CardDTO Card { get; set; }
        public string Buyer { get; set; }
    }
}
