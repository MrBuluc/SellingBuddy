using BasketService.Application.DTOs;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.AddItem
{
    public class AddItemCustomerBasketCommandRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
