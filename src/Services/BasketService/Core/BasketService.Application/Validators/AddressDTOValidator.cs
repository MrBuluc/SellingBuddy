using BasketService.Application.DTOs;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class AddressDTOValidator : AbstractValidator<AddressDTO>
    {
        public AddressDTOValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("Address.City must be not empty.");

            RuleFor(x => x.Street).NotEmpty().WithMessage("Address.Street must be not empty.");

            RuleFor(x => x.State).NotEmpty().WithMessage("Address.State must be not empty.");

            RuleFor(x => x.Country).NotEmpty().WithMessage("Address.Country must be not empty.");

            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Address.ZipCode must be not empty.");
        }
    }
}
