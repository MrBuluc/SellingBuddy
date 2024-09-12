using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Queries.GetById
{
    public class GetCustomerBasketByIdQueryRequest : IRequest<GetCustomerBasketByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
