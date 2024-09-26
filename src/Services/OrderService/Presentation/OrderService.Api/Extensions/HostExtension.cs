using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace OrderService.Api.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using IServiceScope scope = host.Services.CreateScope();
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
                    })
                    .Execute(() => InvokeSeeder(seeder, services.GetService<TContext>()!, services));

                logger.LogInformation("Migrated db");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while migration the db used on context {DbContextName}", typeof(TContext).Name);
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            seeder(context, services);
        }
    }
}
