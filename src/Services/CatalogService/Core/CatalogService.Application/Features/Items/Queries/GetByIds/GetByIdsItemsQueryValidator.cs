using FluentValidation;

namespace CatalogService.Application.Features.Items.Queries.GetByIds
{
    public class GetByIdsItemsQueryValidator : AbstractValidator<GetByIdsItemsQueryRequest>
    {
        public GetByIdsItemsQueryValidator()
        {
            RuleFor(x => x.Ids).NotEmpty().WithMessage("Ids must not be empty.").Must(ids => ids.Split(",").All(id => id.All(char.IsLetterOrDigit))).WithMessage("Ids must be a comma-separated list of numbers");
        }
    }
}
