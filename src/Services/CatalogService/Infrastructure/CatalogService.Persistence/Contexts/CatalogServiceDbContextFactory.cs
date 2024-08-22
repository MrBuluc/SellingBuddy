using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CatalogService.Persistence.Contexts
{
    public class CatalogServiceDbContextFactory : IDesignTimeDbContextFactory<CatalogServiceDbContext>
    {
        public CatalogServiceDbContext CreateDbContext(string[] args) => new(new DbContextOptionsBuilder<CatalogServiceDbContext>().UseSqlServer(Configuration.ConnectionString).Options);
    }
}
