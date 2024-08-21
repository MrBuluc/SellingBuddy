using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetById
{
    public class GetByIdItemQueryRequest : IRequest<GetByIdItemQueryResponse>
    {
        public int Id { get; set; }
    }
}
