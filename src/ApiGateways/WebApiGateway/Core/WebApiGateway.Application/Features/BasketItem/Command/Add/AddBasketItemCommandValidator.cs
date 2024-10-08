﻿using FluentValidation;

namespace WebApiGateway.Application.Features.BasketItem.Command.Add
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommandRequest>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage("CatalogItemId must be not empty.")
                .GreaterThan(0).WithMessage("CatalogItemId must be a pozitive number.");

            RuleFor(x => x.Quantity).NotNull().WithMessage("Quantity must be not empty.")
                .GreaterThan(0).WithMessage("Quantity must be a pozitive number.");
        }
    }
}
