using MediatR;

namespace WebApiGateway.Application.Features.BasketItem.Command.Add
{
    public class AddBasketItemCommandRequest : IRequest<Unit>
    {
        public int CatalogItemId { get; set; }
        public string BasketId { get; set; }
        public int Quantity { get; set; }
    }
}
