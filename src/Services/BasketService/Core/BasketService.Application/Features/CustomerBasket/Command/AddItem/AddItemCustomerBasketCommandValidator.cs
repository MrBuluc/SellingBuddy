using BasketService.Application.Validators;
using FluentValidation;

namespace BasketService.Application.Features.CustomerBasket.Command.AddItem
{
    public class AddItemCustomerBasketCommandValidator : AbstractValidator<AddItemCustomerBasketCommandRequest>
    {
        public AddItemCustomerBasketCommandValidator()
        {
            //RuleFor(x => x.Item).SetValidator(new BasketItemDTOValidator());
        }
    }
}
