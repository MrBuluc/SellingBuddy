using MediatR;
using OrderService.Application.Interfaces.UnitOfWorks;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandlers
{
    public class UpdateOrdersBuyerDomainEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<BuyerAndCardVerifiedDomainEvent>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task Handle(BuyerAndCardVerifiedDomainEvent notification, CancellationToken cancellationToken)
        {
            (await unitOfWork.GetOrderReadRepository().GetByIdAsync(notification.OrderId))!.BuyerId = notification.BuyerId;
        }
    }
}
