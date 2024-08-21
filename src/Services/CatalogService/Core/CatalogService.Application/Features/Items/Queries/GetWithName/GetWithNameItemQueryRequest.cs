using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetWithName
{
    public class GetWithNameItemQueryRequest : IRequest<GetWithNameItemQueryResponse>
    {
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
