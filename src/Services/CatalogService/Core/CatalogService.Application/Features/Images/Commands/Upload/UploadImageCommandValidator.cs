using CatalogService.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace CatalogService.Application.Features.Images.Commands.Upload
{
    public class UploadImageCommandValidator : AbstractValidator<UploadImageCommandRequest>
    {
        public UploadImageCommandValidator(IOptionsSnapshot<CatalogSettings> optionsSnapshot)
        {
            RuleFor(x => x.Image).NotNull().WithMessage("Image is required").Must(image => optionsSnapshot.Value.ValidExtensions.Any(image.FileName.EndsWith)).WithMessage($"Invalid image format. Valid formats are: {string.Join(", ", optionsSnapshot.Value.ValidExtensions)}");

            RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("ItemId must be pozitive number.");
        }
    }
}
