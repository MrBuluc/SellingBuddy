using MediatR;

namespace OrderService.Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQueryRequest : IRequest<GetOrderByIdQueryResponse>
    {
        public Guid OrderId { get; set; }
    }
}
