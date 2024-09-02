using FluentValidation;

namespace CatalogService.Application.Features.Items.Commands.Delete
{
    public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommandRequest>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be pozitive number");
            RuleFor(x => x.DeletedBy).NotEmpty().WithMessage("DeletedBy must not be empty.");
        }
    }
}
