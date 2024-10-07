using BasketService.Api.IntegrationEvents.EventHandlers;
using BasketService.Api.IntegrationEvents.Events;
using BasketService.Api.Registrations;
using BasketService.Application;
using BasketService.Application.Exceptions;
using BasketService.Mapper;
using BasketService.Persistence;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using RabbitMQ.Client;

namespace BasketService.Api
{
    public class Startup(IConfiguration configuration, Serilog.ILogger logger)
    {
        private readonly IConfiguration configuration = configuration;
        private readonly Serilog.ILogger logger = logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServiceExt(services);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "SellingBuddy Basket Service",
                    Version = "v1",
                    Description = "SellingBuddy Basket Service API swagger client"
                });
                c.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "'Bearer' yazıp boşluk bıraktıktan sonra Token'ı Girebilirsiniz \r\n\r\n Örneğin: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                c.AddSecurityRequirement(new()
                {
                    {
                        new()
                        {
                            Reference = new()
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddApplication();
            services.AddCustomMapper();
        }

        private void ConfigureServiceExt(IServiceCollection services)
        {
            services.ConfigureAuth(configuration);

            services.AddPersistence(configuration);

            services.ConfigureConsul(configuration);

            services.AddTransient<OrderCreatedIntegrationEventHandler>();
            services.AddSingleton(sp => EventBusFactory.Create(new()
            {
                ConnectionRetryCount = 5,
                SubscriberClientAppName = "BasketService",
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

            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddDebug();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            logger.Information("System up and running - From Configure {TestParam}", "Mr. Bülüç");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.Api v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.RegisterWithConsul(lifetime, configuration);

            ConfigureSubscription(app.ApplicationServices);

            app.ConfigureExceptionHandlingMiddleware();
        }

        private static void ConfigureSubscription(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IEventBus>().Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }
    }
}
