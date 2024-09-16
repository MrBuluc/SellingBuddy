using BasketService.Api;
using Serilog;

internal class Program
{
    private static string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static IConfiguration configuration
    {
        get => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static IConfiguration serilogConfiguration
    {
        get => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json", optional: false)
            .AddJsonFile($"serilog.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static IHost BuildWebHost(IConfiguration configuration, string[] args) => Host.CreateDefaultBuilder()
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateOnBuild = false;
            options.ValidateScopes = false;
        })
        .ConfigureAppConfiguration(iConfigurationBuilder => iConfigurationBuilder.AddConfiguration(configuration))
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.ConfigureLogging(iLoggingBuilder => iLoggingBuilder.ClearProviders());
        })
        .UseSerilog()
        .Build();

    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(serilogConfiguration)
            .CreateLogger();

        Log.Logger.Information("Application is Running...");

        BuildWebHost(configuration, args).Run();
    }
}