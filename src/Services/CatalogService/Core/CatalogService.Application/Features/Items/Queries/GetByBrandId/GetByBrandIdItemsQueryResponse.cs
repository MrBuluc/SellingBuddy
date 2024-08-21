using CatalogService.Application.DTOs;

namespace CatalogService.Application.Features.Items.Queries.GetByBrandId
{
    public class GetByBrandIdItemsQueryResponse
    {
        public long Count { get; set; }
        public IList<ItemDTO> Items { get; set; }
    }
}
