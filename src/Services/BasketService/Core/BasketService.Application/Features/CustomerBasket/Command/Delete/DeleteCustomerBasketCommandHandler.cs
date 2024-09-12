using BasketService.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.Delete
{
    public class DeleteCustomerBasketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerBasketCommandRequest, Unit>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteCustomerBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.GetWriteRepository<Domain.Entities.CustomerBasket>().DeleteAsync(request.BuyerId);

            return Unit.Value;
        }
    }
}
