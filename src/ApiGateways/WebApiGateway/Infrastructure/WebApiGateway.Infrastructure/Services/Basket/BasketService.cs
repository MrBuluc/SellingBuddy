using WebApiGateway.Application.Interfaces.Basket;
using WebApiGateway.Application.Models.Basket;
using WebApiGateway.Infrastructure.Extensions;

namespace WebApiGateway.Infrastructure.Services.Basket
{
    public class BasketService(IHttpClientFactory httpClientFactory) : IBasketService
    {
        private readonly HttpClient client = httpClientFactory.CreateClient("basket");

        public async Task<BasketData?> GetById(string id) => await client.GetResponseAsync<BasketData>(id);

        public async Task<BasketData?> UpdateAsync(BasketData basket) => await client.PostGetResponseAsync<BasketData, BasketData>("update", basket);
    }
}
