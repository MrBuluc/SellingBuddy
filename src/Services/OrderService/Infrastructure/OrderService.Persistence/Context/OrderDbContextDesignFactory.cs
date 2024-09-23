using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderService.Persistence.Context
{
    public class OrderDbContextDesignFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {

        public OrderDbContext CreateDbContext(string[] args) => new(new DbContextOptionsBuilder<OrderDbContext>().UseSqlServer(Configuration.ConnectionString).Options);
    }
}
