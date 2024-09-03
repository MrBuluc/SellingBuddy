using IdentityService.Persistence.Contexts;
using IdentityService.Persistence.Extensions;
using Serilog;

namespace IdentityService.Api
{
    public class Program
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

        public static IHost BuildWebHost(IConfiguration configuration) => Host.CreateDefaultBuilder()
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
            IHost host = BuildWebHost(Configuration);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(SerilogConfiguration)
                .CreateLogger();

            host.MigrateDbContext<IdentityServiceDbContext>();

            Log.Logger.Information("Identity Service Application is Running...");

            host.Run();
        }
    }
}