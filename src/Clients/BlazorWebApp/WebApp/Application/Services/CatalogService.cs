using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models;
using WebApp.Domain.Models.Catalog;
using WebApp.Extensions;

namespace WebApp.Application.Services
{
    public class CatalogService(HttpClient httpClient) : ICatalogService
    {
        private readonly HttpClient httpClient = httpClient;

        public async Task<PaginatedItemsViewModel<CatalogItem>?> GetCatalogItemsAsync() => await httpClient.GetResponseAsync<PaginatedItemsViewModel<CatalogItem>>("/catalog/items");
    }
}
