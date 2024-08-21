using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<CatalogSettings>(configuration.GetSection("CatalogSettings"));
            services.AddTransient<IPictureService, PictureService>();
        }
    }
}
