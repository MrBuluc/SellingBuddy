using BasketService.Application.DTOs;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class BasketItemDTOValidator : AbstractValidator<BasketItemDTO>
    {
        public BasketItemDTOValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("BasketItem.Quantity must be a positive number.");

            RuleFor(x => x.Product).SetValidator(new ProductDTOValidator());
        }
    }
}
