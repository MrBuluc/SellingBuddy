using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Repositories
{
    public class WriteRepository<T>(DbContext context) : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext context = context;
        private DbSet<T> Table { get => context.Set<T>(); }

        public async Task AddAsync(T entity, CancellationToken token)
        {
            entity.CreatedDate = DateTime.Now;
            await Table.AddAsync(entity, token);
        }

        public bool HardDelete(T entity)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State is EntityState.Deleted;
        }

        public async Task SoftDeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            await UpdateAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}
