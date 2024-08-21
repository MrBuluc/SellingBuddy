using CatalogService.Application.DTOs;

namespace CatalogService.Application.Features.Items.Queries.GetWithName
{
    public class GetWithNameItemQueryResponse
    {
        public long Count { get; set; }
        public IList<ItemDTO> Items { get; set; }
    }
}
