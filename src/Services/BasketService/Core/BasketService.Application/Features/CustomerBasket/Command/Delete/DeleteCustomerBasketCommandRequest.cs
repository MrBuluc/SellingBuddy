using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.Delete
{
    public class DeleteCustomerBasketCommandRequest : IRequest<Unit>
    {
        public string BuyerId { get; set; }
    }
}
