using BasketService.Application.Validators;
using FluentValidation;

namespace BasketService.Application.Features.CustomerBasket.Command.Checkout
{
    public class CheckoutCustomerBasketCommandValidator : AbstractValidator<CheckoutCustomerBasketCommandRequest>
    {
        public CheckoutCustomerBasketCommandValidator()
        {
            RuleFor(x => x.Address).SetValidator(new AddressDTOValidator());

            RuleFor(x => x.Card).SetValidator(new CardDTOValidator());

            RuleFor(x => x.Buyer).NotEmpty().WithMessage("Buyer must be not empty.");
        }
    }
}
