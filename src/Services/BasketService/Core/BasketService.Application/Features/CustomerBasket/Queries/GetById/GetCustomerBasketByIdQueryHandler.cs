using BasketService.Application.Bases;
using BasketService.Application.DTOs;
using BasketService.Application.Interfaces.AutoMapper;
using BasketService.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Queries.GetById
{
    public class GetCustomerBasketByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetCustomerBasketByIdQueryRequest, GetCustomerBasketByIdQueryResponse>
    {
        public async Task<GetCustomerBasketByIdQueryResponse> Handle(GetCustomerBasketByIdQueryRequest request, CancellationToken cancellationToken) => new GetCustomerBasketByIdQueryResponse
        {
            CustomerBasket = mapper.Map<CustomerBasketDTO, Domain.Entities.CustomerBasket>(await unitOfWork.GetReadRepository<Domain.Entities.CustomerBasket>().GetAsync(request.Id) ?? new Domain.Entities.CustomerBasket
            {
                BuyerId = request.Id,
            })
        };
    }
}
