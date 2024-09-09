using WebApiGateway.Application.Models.Catalog;

namespace WebApiGateway.Application.Interfaces.Catalog
{
    public interface ICatalogService
    {
        Task<CatalogItem?> GetItemAsync(int id);
    }
}
