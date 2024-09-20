using MediatR;
using OrderService.Application.Interfaces.UnitOfWorks;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandlers
{
    public class OrderStartedDomainEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            Buyer? buyer = await unitOfWork.GetReadRepository<Buyer>().GetSingleAsync(b => b.Name == notification.UserName, b => b.Cards);

            bool buyerOriginallyExisted = buyer is not null;

            if (!buyerOriginallyExisted)
            {
                buyer = new(notification.UserName);
            }

            buyer!.VerifyAndAddCard(notification.Card.Number, notification.Card.SecurityNumber, notification.Card.HolderName, notification.Card.Expiration, notification.Order.Id);

            Interfaces.Repositories.IWriteRepository<Buyer> writeRepository = unitOfWork.GetWriteRepository<Buyer>();
            Buyer buyerUpdated = buyerOriginallyExisted ? writeRepository.Update(buyer) : await writeRepository.AddAsync(buyer);

            await unitOfWork.SaveEntitiesAsync(cancellationToken);

            // order status changed event may be fired here
        }
    }
}
