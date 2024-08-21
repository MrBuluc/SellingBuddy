using MediatR;

namespace CatalogService.Application.Features.Images.Queries.Get
{
    public class GetImageQueryRequest : IRequest<GetImageQueryResponse>
    {
        public int ItemId { get; set; }
    }
}
