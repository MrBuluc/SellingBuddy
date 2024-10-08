﻿using MediatR;
using WebApiGateway.Application.Exceptions;
using WebApiGateway.Application.Interfaces.Basket;
using WebApiGateway.Application.Interfaces.Catalog;
using WebApiGateway.Application.Models.Catalog;

namespace WebApiGateway.Application.Features.BasketItem.Command.Add
{
    public class AddBasketItemCommandHandler(IBasketService basketService, ICatalogService catalogService) : IRequestHandler<AddBasketItemCommandRequest, Unit>
    {
        private readonly IBasketService basketService = basketService;
        private readonly ICatalogService catalogService = catalogService;

        public async Task<Unit> Handle(AddBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            CatalogItem item = await catalogService.GetItemAsync(request.ProductId) ?? throw new CatalogItemNotFoundException();

            Models.Basket.BasketData currentBasket = await basketService.GetMyBasket();

            Models.Basket.BasketItem? basketItem = currentBasket.Items.SingleOrDefault(i => i.Product.Id == item.Id);

            if (basketItem is not null)
            {
                basketItem.Quantity += request.Quantity;
            }
            else
            {
                currentBasket.Items.Add(new()
                {
                    Product = item,
                    Quantity = request.Quantity,
                    Id = Guid.NewGuid().ToString()
                });
            }

            Models.Basket.BasketData? basketData = await basketService.UpdateAsync(currentBasket);

            return Unit.Value;
        }
    }
}
