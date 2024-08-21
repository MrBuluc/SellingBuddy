using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IPictureService
    {
        IList<Item> ChangeUriPlaceholder(IList<Item> items);
        string GetMimeType(string extension);
    }
}
