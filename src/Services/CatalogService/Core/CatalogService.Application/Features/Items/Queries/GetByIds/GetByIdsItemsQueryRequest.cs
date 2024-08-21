using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetByIds
{
    public class GetByIdsItemsQueryRequest : IRequest<IList<GetByIdsItemsQueryResponse>>
    {
        public string Ids { get; set; }
    }
}
