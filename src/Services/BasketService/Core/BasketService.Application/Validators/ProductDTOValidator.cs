using BasketService.Application.DTOs;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Product.Id must be a positive number.");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product.UnitPrice must be a positive number.");
        }
    }
}
