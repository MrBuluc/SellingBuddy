using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Web.ApiGateway.Consul;
using WebApiGateway.Application;
using WebApiGateway.Application.Exceptions;
using WebApiGateway.Infrastructure;

namespace Web.ApiGateway
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());

            services.AddHealthChecksUI().AddInMemoryStorage();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "SellingBuddy WebApiGateway",
                    Version = "v1",
                    Description = "SellingBuddy Web.ApiGateway API swagger client."
                });
            });

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddOcelot().AddConsul<MyConsulServiceBuilder>();

            ConfigureHttpClient(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        private void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientDelegatingHandler>();

            services.AddHttpClient("basket", c =>
            {
                c.BaseAddress = new(Configuration["urls:basket"]);
            })
                .AddHttpMessageHandler<HttpClientDelegatingHandler>();

            services.AddHttpClient("catalog", c =>
            {
                c.BaseAddress = new(Configuration["urls:catalog"]);
            })
                .AddHttpMessageHandler<HttpClientDelegatingHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.ApiGateway v1"));
            }

            app.ConfigureExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();
            });

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

            await app.UseOcelot();
        }
    }
}
