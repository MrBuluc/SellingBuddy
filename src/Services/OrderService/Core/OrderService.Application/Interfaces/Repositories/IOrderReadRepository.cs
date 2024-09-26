using OrderService.Domain.AggregateModels.OrderAggregate;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
    }
}
