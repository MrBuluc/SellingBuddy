using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories;

namespace CatalogService.Persistence.UnitOfWorks
{
    public class UnitOfWork(CatalogServiceDbContext context) : IUnitOfWork
    {
        private readonly CatalogServiceDbContext context = context;

        public async ValueTask DisposeAsync() => await context.DisposeAsync();

        public async Task<int> SaveAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(context);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(context);
    }
}
