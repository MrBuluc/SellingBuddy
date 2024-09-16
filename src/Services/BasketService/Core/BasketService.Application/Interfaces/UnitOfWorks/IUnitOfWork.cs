using BasketService.Application.Interfaces.Repositories;
using BasketService.Domain.Common;

namespace BasketService.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new();
        IWriteRepository<T> GetWriteRepository<T>() where T: class, IEntityBase, new();
    }
}
