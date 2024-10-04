using MediatR;

namespace WebApiGateway.Application.Features.BasketItem.Command.Add
{
    public class AddBasketItemCommandRequest : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
