using MediatR;

namespace CatalogService.Application.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQueryRequest : IRequest<IList<GetAllBrandsQueryResponse>>
    {
    }
}
