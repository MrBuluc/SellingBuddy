using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Common;

namespace OrderService.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity;
        IOrderReadRepository GetOrderReadRepository();
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
