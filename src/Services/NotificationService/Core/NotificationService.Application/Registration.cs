using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NotificationService.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
