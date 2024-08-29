using Microsoft.AspNetCore;
using PaymentService.Api;
using Serilog;

internal class Program
{
    private static readonly string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

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

    [Obsolete]
    public static IWebHost BuildWebHost(IConfiguration configuration) => WebHost.CreateDefaultBuilder()
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateOnBuild = false;
            options.ValidateScopes = false;
        })
        .ConfigureAppConfiguration(i => i.AddConfiguration(Configuration))
        .UseStartup<Startup>()
        .ConfigureLogging(i => i.ClearProviders())
        .UseSerilog()
        .Build();

    [Obsolete]
    private static void Main(string[] args)
    {
        IWebHost host = BuildWebHost(Configuration);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(SerilogConfiguration)
            .CreateLogger();

        Log.Logger.Information("PaymentService app is Running...");

        host.Run();
    }
}