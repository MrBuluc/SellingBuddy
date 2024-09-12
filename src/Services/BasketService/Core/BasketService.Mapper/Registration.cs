using BasketService.Application.Interfaces.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
