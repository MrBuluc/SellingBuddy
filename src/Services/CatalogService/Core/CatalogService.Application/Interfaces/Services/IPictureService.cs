using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IPictureService
    {
        IList<Item> ChangeUriPlaceholder(IList<Item> items);
        string GetMimeType(string extension);
        void SaveIFormFile(IFormFile image, string path);
    }
}
