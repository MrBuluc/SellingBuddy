using FluentValidation;

namespace CatalogService.Application.Features.Items.Command.Update
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommandRequest>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be pozitive number.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must not empty.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not br empty.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be pozitive number.");
            
            string[] validExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp"];
            RuleFor(x => x.PictureFileName).NotEmpty().WithMessage("PictureFileName must not be empty.").Must(p => validExtensions.Contains(Path.GetExtension(p)?.ToLower())).WithMessage($"PictureFileName must have a valid extension (e.g., {string.Join(", ", validExtensions)})");

            RuleFor(x => x.AvailableStock).GreaterThan(0).WithMessage("AvailableStock must be pozitive number.");
            RuleFor(x => x.TypeId).GreaterThan(0).WithMessage("TypeId must be pozitive number.");
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("BrandId must be pozitive number.");
            RuleFor(x => x.UpdatedBy).NotEmpty().WithMessage("UpdatedBy must not be empty.");
        }
    }
}
