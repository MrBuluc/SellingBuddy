using BasketService.Application.DTOs;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.AddItem
{
    public class AddItemCustomerBasketCommandRequest : IRequest<Unit>
    {
        public BasketItemDTO Item { get; set; }
        public Guid UserId { get; set; }
    }
}
