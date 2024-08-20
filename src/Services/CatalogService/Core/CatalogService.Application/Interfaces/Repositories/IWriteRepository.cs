using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity, CancellationToken token);
        Task<T> UpdateAsync(T entity);
        bool HardDelete(T entity);
        Task SoftDeleteAsync(T entity);
    }
}
