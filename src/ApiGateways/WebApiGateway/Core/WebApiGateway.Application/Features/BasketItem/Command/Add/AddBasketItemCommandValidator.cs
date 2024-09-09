using FluentValidation;

namespace WebApiGateway.Application.Features.BasketItem.Command.Add
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommandRequest>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CatalogItemId).NotNull().WithMessage("CatalogItemId must be not empty.")
                .GreaterThan(0).WithMessage("CatalogItemId must be a pozitive number.");

            RuleFor(x => x.BasketId).NotNull().WithMessage("BasketId must be not empty.");

            RuleFor(x => x.Quantity).NotNull().WithMessage("Quantity must be not empty.")
                .GreaterThan(0).WithMessage("Quantity must be a pozitive number.");
        }
    }
}
