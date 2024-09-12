using BasketService.Domain.Common;

namespace BasketService.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
    }
}
