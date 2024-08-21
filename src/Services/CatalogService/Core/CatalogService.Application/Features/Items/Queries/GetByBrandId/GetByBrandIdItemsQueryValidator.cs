using FluentValidation;

namespace CatalogService.Application.Features.Items.Queries.GetByBrandId
{
    public class GetByBrandIdItemsQueryValidator : AbstractValidator<GetByBrandIdItemsQueryRequest>
    {
        public GetByBrandIdItemsQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex have to be pozitive number.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize have to be pozitive number");
        }
    }
}
