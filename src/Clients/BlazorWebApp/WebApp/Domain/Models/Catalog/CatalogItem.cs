namespace WebApp.Domain.Models.Catalog
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public int AvailableStock { get; set; }
        public bool OnReorder { get; set; }

        public int TypeId { get; set; }
        public CatalogType Type { get; set; }
        public int BrandId { get; set; }
        public CatalogBrand Brand { get; set; }
    }
}
