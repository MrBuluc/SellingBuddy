using FluentValidation;

namespace CatalogService.Application.Features.Items.Queries.GetById
{
    public class GetByIdItemQueryValidator : AbstractValidator<GetByIdItemQueryRequest>
    {
        public GetByIdItemQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id have to be pozitive number");
        }
    }
}
