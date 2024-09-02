using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application;
using NotificationService.ConsoleApp.IntegrationEvents.EventHandlers;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using NotificationService.Infrastructure;
using RabbitMQ.Client;
using Serilog;

internal class Program
{
    private static string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    private static IConfiguration Configuration
    {
        get => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
    private static IConfiguration SerilogConfiguration
    {
        get => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json", optional: false)
            .AddJsonFile($"serilog.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static void Main(string[] args)
    {
        ServiceCollection services = new();
        ConfigureServices(services);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(SerilogConfiguration)
            .CreateLogger();

        IEventBus eventBus = services.BuildServiceProvider().GetRequiredService<IEventBus>();
        eventBus.Subscribe<OrderPaymentSuccessIntegrationEvent, OrderPaymentSuccessIntegrationEventHandler>();
        eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();

        Log.Logger.Information("NotificationService Application is Running...");
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddTransient<OrderPaymentFailedIntegrationEventHandler>();
        services.AddTransient<OrderPaymentSuccessIntegrationEventHandler>();

        services.AddSingleton(sp => EventBusFactory.Create(new()
        {
            ConnectionRetryCount = 5,
            SubscriberClientAppName = "NotificationService",
            EventBusType = EventBusType.RabbitMQ,
            EventNameSuffix = "IntegrationEvent",
            Connection = new ConnectionFactory()
            {
                HostName = "207.154.222.131",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            }
        }, sp)!);

        services.AddApplication();
        services.AddInfrastructure(Configuration);
    }
}