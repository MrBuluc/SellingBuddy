using FluentValidation;

namespace CatalogService.Application.Features.Items.Queries.GetWithName
{
    public class GetWithNameItemQueryValidator : AbstractValidator<GetWithNameItemQueryRequest>
    {
        public GetWithNameItemQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex have to be pozitive number.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize have to be pozitive number");
        }
    }
}
