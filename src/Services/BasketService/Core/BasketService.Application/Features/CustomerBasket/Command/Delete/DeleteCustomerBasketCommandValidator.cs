using FluentValidation;

namespace BasketService.Application.Features.CustomerBasket.Command.Delete
{
    public class DeleteCustomerBasketCommandValidator : AbstractValidator<DeleteCustomerBasketCommandRequest>
    {
        public DeleteCustomerBasketCommandValidator()
        {
            RuleFor(x => x.BuyerId).NotEmpty().WithMessage("BuyerId must be not empty.");
        }
    }
}
