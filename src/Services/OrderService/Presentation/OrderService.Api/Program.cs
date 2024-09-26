using Microsoft.AspNetCore;
using OrderService.Api;
using OrderService.Api.Extensions;
using OrderService.Persistence.Context;
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

    public static IHost BuildWebHost(IConfiguration configuration) => Host.CreateDefaultBuilder()
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateOnBuild = false;
            options.ValidateScopes = false;
        })
        .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.ConfigureLogging(i => i.ClearProviders());
        })
        .UseSerilog()
        .Build();

    private static void Main(string[] args)
    {
        IHost host = BuildWebHost(Configuration);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(SerilogConfiguration)
            .CreateLogger();

        host.MigrateDbContext<OrderDbContext>((context, services) =>
        {
            (new OrderDbContextSeed()).SeedAsync(context, services.GetService<ILogger<OrderDbContext>>()!).Wait();
        });

        Log.Information("Application is Running...");

        host.Run();
    }
}