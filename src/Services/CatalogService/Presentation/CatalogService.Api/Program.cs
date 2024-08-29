using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Extensions;
using Serilog;

namespace CatalogService.Api
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
            .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseWebRoot("Pics");
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
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

            host.MigrateDbContext<CatalogServiceDbContext>((context, services) =>
            {
                new CatalogContextSeed()
                .SeedAsync(context, services.GetService<IWebHostEnvironment>(), services.GetService<ILogger<CatalogContextSeed>>())
                .Wait();
            });

            Log.Logger.Information("Catalog Service Application is Running...");

            host.Run();
        }
    }
}