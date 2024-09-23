using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.UnitOfWorks;
using OrderService.Persistence.Context;
using OrderService.Persistence.Repositories;
using OrderService.Persistence.UnitOfWorks;

namespace OrderService.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
                opt.EnableSensitiveDataLogging();
            });

            using OrderDbContext dbContext = new(new DbContextOptionsBuilder<OrderDbContext>().UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]).Options);
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
