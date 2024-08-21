using FluentValidation;

namespace CatalogService.Application.Features.Items.Queries.GetByTypeIdAndBrandId
{
    public class GetByTypeIdAndBrandIdItemQueryValidator : AbstractValidator<GetByTypeIdAndBrandIdItemQueryRequest>
    {
        public GetByTypeIdAndBrandIdItemQueryValidator()
        {
            RuleFor(x => x.TypeId).GreaterThan(0).WithMessage("TypeId have to be pozitive number.");
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex have to be pozitive number.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize have to be pozitive number");
        }
    }
}
