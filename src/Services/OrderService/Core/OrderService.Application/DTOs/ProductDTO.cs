﻿namespace OrderService.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
    }
}
