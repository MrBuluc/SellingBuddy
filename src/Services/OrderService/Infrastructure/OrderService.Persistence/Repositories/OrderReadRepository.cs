using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Persistence.Context;
using System.Linq.Expressions;

namespace OrderService.Persistence.Repositories
{
    public class OrderReadRepository(OrderDbContext dbContext) : ReadRepository<Order>(dbContext), IOrderReadRepository
    {
        private readonly OrderDbContext dbContext = dbContext;

        public override async Task<Order?> GetByIdAsync(Guid id, params Expression<Func<Order, object>>[] includes) => await base.GetByIdAsync(id, includes) ?? dbContext.Orders.Local.FirstOrDefault(o => o.Id == id);
    }
}
