namespace CatalogService.Application.DTOs
{
    public class ItemDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public string? PictureUri { get; set; }
        public int AvailableStock { get; set; }
        public bool OnReorder { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
