using CatalogService.Application.Bases;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetAllBrandsQueryRequest, IList<GetAllBrandsQueryResponse>>
    {
        public async Task<IList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken) => mapper.Map<IList<GetAllBrandsQueryResponse>, IList<Brand>>(await unitOfWork.GetReadRepository<Brand>().GetAllAsync(cancellationToken));
    }
}
