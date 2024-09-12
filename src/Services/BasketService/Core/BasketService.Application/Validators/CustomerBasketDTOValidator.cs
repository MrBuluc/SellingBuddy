using BasketService.Application.DTOs;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class CustomerBasketDTOValidator : AbstractValidator<CustomerBasketDTO>
    {
        public CustomerBasketDTOValidator()
        {
            RuleFor(x => x.BuyerId).NotEmpty().WithMessage("CustomerBasket.BuyerId must be not empty");

            RuleForEach(x => x.Items).SetValidator(new BasketItemDTOValidator());
        }
    }
}
