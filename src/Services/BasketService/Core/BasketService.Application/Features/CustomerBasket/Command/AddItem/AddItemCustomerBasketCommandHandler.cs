using BasketService.Application.Bases;
using BasketService.Application.Interfaces.AutoMapper;
using BasketService.Application.Interfaces.UnitOfWorks;
using BasketService.Domain.Entities;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.AddItem
{
    public class AddItemCustomerBasketCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<AddItemCustomerBasketCommandRequest, Unit>
    {
        public async Task<Unit> Handle(AddItemCustomerBasketCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.CustomerBasket customerBasket = await unitOfWork.GetReadRepository<Domain.Entities.CustomerBasket>().GetAsync(request.UserId.ToString()) ?? new Domain.Entities.CustomerBasket { BuyerId = request.UserId.ToString() };

            customerBasket.Items.Add(mapper.Map<BasketItem, AddItemCustomerBasketCommandRequest>(request));
            await unitOfWork.GetWriteRepository<Domain.Entities.CustomerBasket>().UpdateAsync(customerBasket);

            return Unit.Value;
        }
    }
}
