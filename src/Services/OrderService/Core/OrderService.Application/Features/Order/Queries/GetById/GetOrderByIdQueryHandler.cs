using MediatR;
using OrderService.Application.Bases;
using OrderService.Application.DTOs;
using OrderService.Application.Exceptions;
using OrderService.Application.Interfaces.AutoMapper;
using OrderService.Application.Interfaces.UnitOfWorks;
using OrderService.Domain.AggregateModels.OrderAggregate;

namespace OrderService.Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            mapper.Map<AddressDTO, Address>(new Address());
            mapper.Map<OrderItemDTO, Item>(new Item());
            mapper.Map<ProductDTO, Product>(new Product());

            Domain.AggregateModels.OrderAggregate.Order order = await unitOfWork.GetOrderReadRepository().GetByIdAsync(request.OrderId, o => o.Items, o => o.Status) ?? throw new OrderNotFoundException();
            GetOrderByIdQueryResponse getOrderByIdQueryResponse = mapper.Map<GetOrderByIdQueryResponse, Domain.AggregateModels.OrderAggregate.Order>(order);
            getOrderByIdQueryResponse.Total = order.Items.Sum(i => i.Quantity * i.Product.UnitPrice);
            getOrderByIdQueryResponse.Status = order.Status.Name;

            return getOrderByIdQueryResponse;
        }
    }
}
