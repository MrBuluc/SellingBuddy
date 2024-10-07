using WebApp.Application.Services.DTOs;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models.ViewModels;
using WebApp.Extensions;

namespace WebApp.Application.Services
{
    public class BasketService(HttpClient httpClient, IIdentityService identityService) : IBasketService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IIdentityService identityService = identityService;

        private readonly string baseUrl = "basket/";

        public async Task AddItemToBasketAsync(int productId)
        {
            await httpClient.PostAsync($"api/{baseUrl}item", new
            {
                ProductId = productId,
                Quantity = 1,
            });
        }

        public Task CheckoutAsync(BasketDTO basket) => httpClient.PostAsync($"{baseUrl}checkout", basket);

        public async Task<Basket?> GetBasketAsync() => await httpClient.GetResponseAsync<Basket>($"{baseUrl}{identityService.GetUserId()}");

        public async Task<Basket?> UpdateBasketAsync(Basket basket)
        {
            MyHttpResponseMessage response = await httpClient.PostGetResponseAsync<Basket, Basket>($"{baseUrl}update", basket);

            if (!response.IsSuccessStatusCode && response.Content is not null)
            {
                throw response.Content;
            }
            if (!response.IsSuccessStatusCode || response.Content is null)
            {
                return null;
            }

            return response.Content;
        }
    }
}
