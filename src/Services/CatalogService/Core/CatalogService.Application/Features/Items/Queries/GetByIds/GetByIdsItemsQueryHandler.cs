using CatalogService.Application.Bases;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetByIds
{
    public class GetByIdsItemsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetByIdsItemsQueryRequest, IList<GetByIdsItemsQueryResponse>>
    {
        private readonly IPictureService pictureService = pictureService;

        public async Task<IList<GetByIdsItemsQueryResponse>> Handle(GetByIdsItemsQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<(bool Ok, int Value)> numIds = request.Ids.Split(",").Select(id => (Ok: int.TryParse(id, out int x), Value: x));
            // "1,5,8,ab"
            // ["1", "5", "8", "ab"]
            // [(Ok: T, V: 1), (Ok: T, V: 5), (Ok: T, V: 8), (Ok: F, V: 0)]

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<GetByIdsItemsQueryResponse>();
            }

            return mapper.Map<IList<GetByIdsItemsQueryResponse>, IList<Item>>(pictureService.ChangeUriPlaceholder(await unitOfWork.GetReadRepository<Item>().GetAllAsync(cancellationToken, predicate: x => numIds.Select(nid => nid.Value).Contains(x.Id))));
        }
    }
}
