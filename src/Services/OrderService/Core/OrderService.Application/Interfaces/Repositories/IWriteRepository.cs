using OrderService.Domain.Common;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        T Update(T entity);
    }
}
