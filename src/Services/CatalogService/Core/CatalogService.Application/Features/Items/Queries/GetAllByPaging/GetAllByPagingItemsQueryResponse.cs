using CatalogService.Application.DTOs;

namespace CatalogService.Application.Features.Items.Queries.GetAllByPaging
{
    public class GetAllByPagingItemsQueryResponse
    {
        public long Count { get; set; }
        public IList<ItemDTO> Items { get; set; }
    }
}
