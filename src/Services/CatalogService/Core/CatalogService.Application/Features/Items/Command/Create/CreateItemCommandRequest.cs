﻿using MediatR;

namespace CatalogService.Application.Features.Items.Command.Create
{
    public class CreateItemCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public int BrandId { get; set; }
        public int TypeId { get; set; }
        public string CreatedBy { get; set; }
    }
}
