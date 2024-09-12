using BasketService.Application.Behaviors;
using BasketService.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BasketService.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddTransient<ExceptionMiddleware>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
        }
    }
}
