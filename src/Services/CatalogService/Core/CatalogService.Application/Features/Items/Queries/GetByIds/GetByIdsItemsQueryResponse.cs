namespace CatalogService.Application.Features.Items.Queries.GetByIds
{
    public class GetByIdsItemsQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int AvailableStock { get; set; }
        public bool OnReorder { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }
    }
}
