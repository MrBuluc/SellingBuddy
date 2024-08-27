namespace CatalogService.Api.Models
{
    public class PaginatedItems<TEntity> where TEntity : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<TEntity> Items { get; set; }

        public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<TEntity> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Items = items;
        }
    }
}
