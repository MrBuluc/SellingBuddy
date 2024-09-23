using MediatR;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.UnitOfWorks;
using OrderService.Domain.Common;
using OrderService.Persistence.Context;
using OrderService.Persistence.Extensions;
using OrderService.Persistence.Repositories;

namespace OrderService.Persistence.UnitOfWorks
{
    public class UnitOfWork(OrderDbContext context, IMediator mediator) : IUnitOfWork
    {
        private readonly OrderDbContext context = context;
        private readonly IMediator mediator = mediator;

        public async ValueTask DisposeAsync() => await context.DisposeAsync();

        public IReadRepository<T> GetReadRepository<T>() where T : BaseEntity => new ReadRepository<T>(context);

        public IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity => new WriteRepository<T>(context);

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await mediator.PublishDomainEventsAsync(context);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
