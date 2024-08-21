using FluentValidation;

namespace CatalogService.Application.Features.Images.Queries.Get
{
    public class GetImageQueryValidator : AbstractValidator<GetImageQueryRequest>
    {
        public GetImageQueryValidator()
        {
            RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("ItemId must be pozitive number.");
        }
    }
}
