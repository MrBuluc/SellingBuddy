using BasketService.Application.Bases;
using BasketService.Application.DTOs;
using BasketService.Application.Exceptions;
using BasketService.Application.Interfaces.AutoMapper;
using BasketService.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.Checkout
{
    public class CheckoutCustomerBasketCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<CheckoutCustomerBasketCommandRequest, CheckoutCustomerBasketCommandResponse>
    {
        public async Task<CheckoutCustomerBasketCommandResponse> Handle(CheckoutCustomerBasketCommandRequest request, CancellationToken cancellationToken) => new CheckoutCustomerBasketCommandResponse
        {
            CustomerBasket = mapper.Map<CustomerBasketDTO, Domain.Entities.CustomerBasket>(await unitOfWork.GetReadRepository<Domain.Entities.CustomerBasket>().GetAsync(request.Buyer) ?? throw new CustomerBasketNotFoundException())
        };
    }
}
