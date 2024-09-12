using BasketService.Application.DTOs;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.Update
{
    public class UpdateCustomerBasketCommandRequest : IRequest<UpdateCustomerBasketCommandResponse>
    {
        public CustomerBasketDTO CustomerBasket { get; set; }
    }
}
