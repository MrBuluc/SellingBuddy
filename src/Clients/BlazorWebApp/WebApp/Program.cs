using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp.Application.Services;
using WebApp.Application.Services.Interfaces;
using WebApp.Infrastructure;
using WebApp.Utils;

namespace WebApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddTransient<IIdentityService, IdentityService>();
            builder.Services.AddTransient<ICatalogService, CatalogService>();
            builder.Services.AddTransient<IBasketService, BasketService>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddSingleton<AppStateManager>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiGatewayHttpClient"));

            builder.Services.AddScoped<AuthTokenHandler>();

            builder.Services.AddHttpClient("ApiGatewayHttpClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5117/");
            })
                .AddHttpMessageHandler<AuthTokenHandler>();

            builder.Services.AddBlazorBootstrap();

            await builder.Build().RunAsync();
        }
    }
}