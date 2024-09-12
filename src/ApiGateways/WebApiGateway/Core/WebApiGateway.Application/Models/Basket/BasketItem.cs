﻿using WebApiGateway.Application.Models.Catalog;

namespace WebApiGateway.Application.Models.Basket
{
    public class BasketItem
    {
        public string Id { get; set; }
        public CatalogItem Item { get; set; }
        public int Quantity { get; set; }
    }
}