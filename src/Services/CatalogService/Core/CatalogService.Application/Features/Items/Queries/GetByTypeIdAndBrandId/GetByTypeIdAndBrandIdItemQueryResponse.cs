using CatalogService.Application.DTOs;

namespace CatalogService.Application.Features.Items.Queries.GetByTypeIdAndBrandId
{
    public class GetByTypeIdAndBrandIdItemQueryResponse
    {
        public long Count { get; set; }
        public IList<ItemDTO> Items { get; set; }
    }
}
