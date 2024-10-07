using BasketService.Application.Bases;
using BasketService.Application.Interfaces.AutoMapper;
using BasketService.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.AddItem
{
    public class AddItemCustomerBasketCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<AddItemCustomerBasketCommandRequest, Unit>
    {
        public async Task<Unit> Handle(AddItemCustomerBasketCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.CustomerBasket customerBasket = await unitOfWork.GetReadRepository<Domain.Entities.CustomerBasket>().GetAsync(request.Id.ToString()) ?? new Domain.Entities.CustomerBasket() { BuyerId = request.Id.ToString() };

            
            customerBasket.Items.Add(new()
            {
                Id = request.Id,
                Product = new()
                {
                    Id = request.ProductId
                },
                Quantity = request.Quantity,
            });
            await unitOfWork.GetWriteRepository<Domain.Entities.CustomerBasket>().UpdateAsync(customerBasket);

            return Unit.Value;
        }
    }
}
