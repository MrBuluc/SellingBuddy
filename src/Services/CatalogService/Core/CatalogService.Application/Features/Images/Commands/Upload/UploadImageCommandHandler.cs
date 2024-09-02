using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace CatalogService.Application.Features.Images.Commands.Upload
{
    public class UploadImageCommandHandler(IUnitOfWork unitOfWork, IPictureService pictureService, IWebHostEnvironment environment, IOptionsSnapshot<CatalogSettings> optionsSnapshot) : IRequestHandler<UploadImageCommandRequest, Unit>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IPictureService pictureService = pictureService;
        private readonly IWebHostEnvironment environment = environment;
        private readonly CatalogSettings catalogSettings = optionsSnapshot.Value;

        public async Task<Unit> Handle(UploadImageCommandRequest request, CancellationToken cancellationToken)
        {
            Item item = await unitOfWork.GetReadRepository<Item>().GetAsync(i => i.Id == request.ItemId && i.DeletedDate == null, cancellationToken) ?? throw new ItemNotFoundException(request.ItemId);

            pictureService.SaveIFormFile(request.Image, Path.Combine(environment.ContentRootPath, catalogSettings.PicBaseUrl));

            item.PictureFileName = request.Image.FileName;
            item.UpdatedBy = "UploadImageCommandHandler()";

            await unitOfWork.GetWriteRepository<Item>().UpdateAsync(item);
            await unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
