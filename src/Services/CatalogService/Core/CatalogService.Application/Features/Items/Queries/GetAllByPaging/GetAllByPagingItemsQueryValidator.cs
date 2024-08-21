using FluentValidation;

namespace CatalogService.Application.Features.Items.Queries.GetAllByPaging
{
    public class GetAllByPagingItemsQueryValidator : AbstractValidator<GetAllByPagingItemsQueryRequest>
    {
        public GetAllByPagingItemsQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex have to be pozitive number.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize have to be pozitive number");
        }
    }
}
