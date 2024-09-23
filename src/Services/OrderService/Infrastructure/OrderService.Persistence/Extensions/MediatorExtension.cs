using MediatR;
using OrderService.Domain.Common;
using OrderService.Persistence.Context;

namespace OrderService.Persistence.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishDomainEventsAsync(this IMediator mediator, OrderDbContext context)
        {
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity>> domainEntities = context.ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents is not null && e.Entity.DomainEvents.Any());

            List<INotification> domainEvents = domainEntities.SelectMany(entity => entity.Entity.DomainEvents!).ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (INotification domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
