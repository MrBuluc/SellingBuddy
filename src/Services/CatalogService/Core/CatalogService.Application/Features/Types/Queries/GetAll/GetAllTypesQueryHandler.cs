using CatalogService.Application.Bases;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace CatalogService.Application.Features.Types.Queries.GetAll
{
    public class GetAllTypesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetAllTypesQueryRequest, IList<GetAllTypesQueryResponse>>
    {
        public async Task<IList<GetAllTypesQueryResponse>> Handle(GetAllTypesQueryRequest request, CancellationToken cancellationToken) => mapper.Map<IList<GetAllTypesQueryResponse>, IList<Domain.Entities.Type>>(await unitOfWork.GetReadRepository<Domain.Entities.Type>().GetAllAsync(cancellationToken));
    }
}
