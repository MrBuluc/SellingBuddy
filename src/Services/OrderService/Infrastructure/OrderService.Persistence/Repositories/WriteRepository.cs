using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Common;

namespace OrderService.Persistence.Repositories
{
    public class WriteRepository<T>(DbContext dbContext) : IWriteRepository<T> where T : BaseEntity
    {
        public virtual async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
