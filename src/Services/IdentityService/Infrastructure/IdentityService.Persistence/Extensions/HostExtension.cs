using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace IdentityService.Persistence.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating db associated with context {DbContextName}", typeof(TContext).Name);

                    Policy.Handle<SqlException>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8),
                        }).Execute(() => MigrateDb(services.GetService<TContext>()));

                    logger.LogInformation("Migrated db associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the db used on context {DbContextName}", typeof(TContext).Name);
                }
            }

            return host;
        }

        private static void MigrateDb<TContext>(TContext? context) where TContext : DbContext
        {
            if (context is not null)
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}
