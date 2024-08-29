using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CatalogService.Persistence.Repositories
{
    public class ReadRepository<T>(DbContext context) : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext context = context;
        private DbSet<T> Table { get => context.Set<T>(); }

        public async Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> queryable = Table;
            queryable = queryable.AsNoTracking();
            if (predicate is not null) queryable = queryable.Where(predicate);
            return await queryable.CountAsync(cancellationToken);
        }

        public async Task<IList<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null) queryable = orderBy(queryable);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<IList<T>> GetAllAsyncByPaging(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null) queryable = orderBy(queryable);
            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);

            queryable = queryable.Where(predicate);
            return await queryable.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
