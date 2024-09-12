using BasketService.Domain.Common;

namespace BasketService.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class, IEntityBase, new()
    {
        Task<T?> GetAsync(string id);

        IEnumerable<string>? GetUsers();
    }
}
