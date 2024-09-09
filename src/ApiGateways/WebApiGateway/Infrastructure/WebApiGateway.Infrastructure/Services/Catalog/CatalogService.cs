using WebApiGateway.Application.Interfaces.Catalog;
using WebApiGateway.Application.Models.Catalog;
using WebApiGateway.Infrastructure.Extensions;

namespace WebApiGateway.Infrastructure.Services.Catalog
{
    public class CatalogService(IHttpClientFactory httpClientFactory) : ICatalogService
    {
        private readonly HttpClient client = httpClientFactory.CreateClient("catalog");

        public async Task<CatalogItem?> GetItemAsync(int id) => await client.GetResponseAsync<CatalogItem>($"items/{id}");
    }
}
