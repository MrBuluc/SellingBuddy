using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces.Services;
using NotificationService.Infrastructure.MailServices;

namespace NotificationService.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IMailService, MailService>();
        }
    }
}
