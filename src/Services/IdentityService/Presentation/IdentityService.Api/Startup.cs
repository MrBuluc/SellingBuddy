using IdentityService.Application;
using IdentityService.Persistence;
using System.Reflection;
using IdentityService.Mapper;
using IdentityService.Infrastructure;
using IdentityService.Application.Exceptions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace IdentityService.Api
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
                    Title = "SellingBuddy Identity Service",
                    Version = "v1",
                    Description = "SellingBuddy Identity Service API swagger client."
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

            services.AddPersistence(Configuration, typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
            services.AddApplication();
            services.AddCustomMapper();
            services.AddInfrastructure(Configuration);

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService.Api v1"));
            }

            app.ConfigureExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/liveness", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self"),
                    ResponseWriter = async (context, healthReport) =>
                    {
                        await Task.CompletedTask;
                    }
                });
            });
        }
    }
}
