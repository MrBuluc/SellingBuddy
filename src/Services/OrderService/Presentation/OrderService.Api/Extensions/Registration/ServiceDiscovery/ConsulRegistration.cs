using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace OrderService.Api.Extensions.Registration.ServiceDiscovery
{
    public static class ConsulRegistration
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(iServiceProvider => new(consulConfig =>
            {
                consulConfig.Address = new(configuration["ConsulConfig:Address"] ?? throw GenerateException("ConsulConfig:Address"));
            }));

            return services;
        }

        private static Exception GenerateException(string key) => new($"Configure {key} Not Found");

        public static async Task<IApplicationBuilder> RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            IConsulClient consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

            ILogger<IApplicationBuilder> logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>()
                .CreateLogger<IApplicationBuilder>();

            Uri uri = new(((app.Properties["server.Features"] as FeatureCollection ?? throw GenerateException("server.Features"))
                .Get<IServerAddressesFeature>() ?? throw GenerateException("IServerAddressesFeature"))
                .Addresses.First());
            string serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName") ?? throw GenerateException("ConsulConfig:ServiceName");
            string serviceId = configuration.GetValue<string>("ConsulConfig:ServiceId") ?? throw GenerateException("ConsulConfig:ServiceId");

            AgentServiceRegistration registration = new()
            {
                ID = serviceId,
                Name = serviceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = new[] { serviceName, serviceId }
            };

            logger.LogInformation("Registering with Consul");
            await consulClient.Agent.ServiceDeregister(registration.ID);
            await consulClient.Agent.ServiceRegister(registration);

            lifetime.ApplicationStopping.Register(async () =>
            {
                logger.LogInformation("Deregistering from Consul");
                await consulClient.Agent.ServiceDeregister(registration.ID);
            });

            return app;
        }
    }
