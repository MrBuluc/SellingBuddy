using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetByTypeIdAndBrandId
{
    public class GetByTypeIdAndBrandIdItemQueryRequest : IRequest<GetByTypeIdAndBrandIdItemQueryResponse>
    {
        public int TypeId { get; set; }
        public int? BrandId { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
