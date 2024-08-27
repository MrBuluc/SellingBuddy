using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using PaymentService.Api.IntegrationEvents.EventHandlers;
using PaymentService.Api.IntegrationEvents.Events;
using RabbitMQ.Client;

namespace PaymentService.Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "SellingBuddy Payment Service",
                    Version = "v1",
                    Description = "SellingBuddy Payment Service API swagger client."
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

            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddDebug();
            });

            services.AddTransient<OrderStartedIntegrationEventHandler>();
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(new()
                {
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "PaymentService",
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    Connection = new ConnectionFactory()
                    {
                        HostName = "207.154.222.131",
                        Port = 5672,
                        UserName = "guest",
                        Password = "guest"
                    }
                }, sp)!;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentService.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetRequiredService<IEventBus>().Subscribe<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();
        }
    }
}
