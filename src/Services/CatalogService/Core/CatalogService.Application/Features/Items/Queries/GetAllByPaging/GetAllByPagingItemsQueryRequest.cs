using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetAllByPaging
{
    public class GetAllByPagingItemsQueryRequest : IRequest<GetAllByPagingItemsQueryResponse>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
