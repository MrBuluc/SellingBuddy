using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace CatalogService.Application.Features.Images.Queries.Get
{
    public class GetImageQueryHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IPictureService pictureService) : IRequestHandler<GetImageQueryRequest, GetImageQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
        private readonly IPictureService pictureService = pictureService;

        public async Task<GetImageQueryResponse> Handle(GetImageQueryRequest request, CancellationToken cancellationToken)
        {
            Item item = await unitOfWork.GetReadRepository<Item>().GetAsync(i => i.Id == request.ItemId, cancellationToken) ?? throw new ItemNotFoundException(request.ItemId);

            return new()
            {
                Buffer = await File.ReadAllBytesAsync(Path.Combine(webHostEnvironment.WebRootPath, item.PictureFileName), cancellationToken),
                MimeType = pictureService.GetMimeType(Path.GetExtension(item.PictureFileName)),
            };
        }
    }
}
