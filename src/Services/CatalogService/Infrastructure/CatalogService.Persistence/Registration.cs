using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CatalogService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Persistence.Repositories;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Persistence.UnitOfWorks;

namespace CatalogService.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, string? assemblyName)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<CatalogServiceDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(assemblyName);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    })
                    .UseInternalServiceProvider(services.BuildServiceProvider());
                });

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
