using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using OrderService.Api.Extensions.Registration.EventHandlerRegistration;
using OrderService.Api.Extensions.Registration.ServiceDiscovery;
using OrderService.Api.IntegrationEvents.EventHandlers;
using OrderService.Api.IntegrationEvents.Events;
using OrderService.Application;
using OrderService.Application.Exceptions;
using OrderService.Mapper;
using OrderService.Persistence;
using RabbitMQ.Client;

namespace OrderService.Api
{
    public class Startup(IConfiguration configuration)
    {
        private readonly IConfiguration configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "SellingBuddy Order Service",
                    Version = "v1",
                    Description = "SellingBuddy Order Service API swagger client."
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

            ConfigureService(services);
        }

        private void ConfigureService(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddApplication()
                .AddPersistence(configuration)
                .ConfigureEventHandlers()
                .AddServiceDiscovery(configuration)
                .AddCustomMapper();

            services.AddSingleton(sp => EventBusFactory.Create(new()
            {
                ConnectionRetryCount = 5,
                SubscriberClientAppName = "OrderService",
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

            //services.ConfigureAuth(configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderService.Api v1"));
            }

            app.ConfigureExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBusForSubscription(app);

            //await app.RegisterWithConsul(lifetime, configuration);
        }

        private static void ConfigureEventBusForSubscription(IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<IEventBus>().Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }
    }
}
