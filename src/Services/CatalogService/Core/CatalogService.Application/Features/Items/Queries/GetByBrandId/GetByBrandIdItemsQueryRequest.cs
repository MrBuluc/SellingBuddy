using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetByBrandId
{
    public class GetByBrandIdItemsQueryRequest : IRequest<GetByBrandIdItemsQueryResponse>
    {
        public int? BrandId { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
