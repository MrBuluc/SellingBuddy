using BasketService.Application.DTOs;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class CardDTOValidator : AbstractValidator<CardDTO>
    {
        public CardDTOValidator()
        {
            RuleFor(x => x.Number).NotEmpty().WithMessage("Card.Number must be not empty.");

            RuleFor(x => x.HolderName).NotEmpty().WithMessage("Card.HolderName must be not empty.");

            RuleFor(x => x.Expiration).NotEmpty().WithMessage("Card.Expiration must be not empty.");

            RuleFor(x => x.SecurityNumber).NotEmpty().WithMessage("Card.SecurityNumber must be not empty.");
        }
    }
}
