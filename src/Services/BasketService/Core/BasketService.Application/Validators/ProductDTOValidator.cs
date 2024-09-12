﻿using BasketService.Application.DTOs;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Product.Id must be a positive number.");

            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Product.UnitPrice must be a positive number.");

            RuleFor(x => x.PictureUrl).NotNull().WithMessage("Product.PictureUrl must be not empty.")
                .Must((pictureUrl) => Uri.TryCreate(pictureUrl, UriKind.Absolute, out _)).WithMessage("Product.PictureUrl must be valid.");
        }
    }
}