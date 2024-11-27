using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CatalogService.Infrastructure
{
    public class PictureService(IOptionsSnapshot<CatalogSettings> optionsSnapshot) : IPictureService
    {
        private readonly CatalogSettings settings = optionsSnapshot.Value;

        public IList<Item> ChangeUriPlaceholder(IList<Item> items)
        {
            foreach (Item item in items)
            {
                item.PictureUri = settings.PicBaseUrl + item.PictureFileName;
            }

            return items;
        }

        public string GetMimeType(string extension) => extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };

        public void SaveIFormFile(IFormFile image, string path)
        {
            using FileStream fileStream = File.Create($"{path}\\{image.FileName}");
            image.CopyTo(fileStream);
        }
    }
}
