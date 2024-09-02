using MediatR;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Features.Images.Commands.Upload
{
    public class UploadImageCommandRequest : IRequest<Unit>
    {
        public IFormFile Image { get; set; }
        public int ItemId { get; set; }
    }
}
