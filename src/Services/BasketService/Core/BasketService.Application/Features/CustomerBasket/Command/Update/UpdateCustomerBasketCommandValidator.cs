using BasketService.Application.Validators;
using FluentValidation;

namespace BasketService.Application.Features.CustomerBasket.Command.Update
{
    public class UpdateCustomerBasketCommandValidator : AbstractValidator<UpdateCustomerBasketCommandRequest>
    {
        public UpdateCustomerBasketCommandValidator()
        {
           RuleFor(x => x.CustomerBasket).SetValidator(new CustomerBasketDTOValidator());
        }
    }
}
