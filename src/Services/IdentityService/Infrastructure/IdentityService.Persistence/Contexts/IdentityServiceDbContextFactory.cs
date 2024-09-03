using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.Persistence.Contexts
{
    public class IdentityServiceDbContextFactory : IDesignTimeDbContextFactory<IdentityServiceDbContext>
    {
        public IdentityServiceDbContext CreateDbContext(string[] args) => new(new DbContextOptionsBuilder<IdentityServiceDbContext>().UseSqlServer(Configuration.ConnectionString).Options);
    }
}
