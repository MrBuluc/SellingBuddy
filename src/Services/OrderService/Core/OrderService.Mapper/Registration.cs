using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.AutoMapper;

namespace OrderService.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
