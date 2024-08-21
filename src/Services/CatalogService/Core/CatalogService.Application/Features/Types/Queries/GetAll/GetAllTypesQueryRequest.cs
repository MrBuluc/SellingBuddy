using MediatR;

namespace CatalogService.Application.Features.Types.Queries.GetAll
{
    public class GetAllTypesQueryRequest : IRequest<IList<GetAllTypesQueryResponse>>
    {
    }
}
