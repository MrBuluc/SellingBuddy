using OrderService.Domain.Common;
using System.Linq.Expressions;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

    }
}
