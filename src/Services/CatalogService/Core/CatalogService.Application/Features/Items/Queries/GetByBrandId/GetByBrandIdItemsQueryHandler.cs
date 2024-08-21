using CatalogService.Application.Bases;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetByBrandId
{
    public class GetByBrandIdItemsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetByBrandIdItemsQueryRequest, GetByBrandIdItemsQueryResponse>
    {
        private readonly IPictureService pictureService = pictureService;

        public async Task<GetByBrandIdItemsQueryResponse> Handle(GetByBrandIdItemsQueryRequest request, CancellationToken cancellationToken)
        {
            IReadRepository<Item> readRepository = unitOfWork.GetReadRepository<Item>();

            return new()
            {
                Count = await readRepository.CountAsync(cancellationToken, predicate: i => Predicate(i, request)),
                Items = mapper.Map<IList<ItemDTO>, IList<Item>>(pictureService.ChangeUriPlaceholder(await readRepository.GetAllAsyncByPaging(cancellationToken, predicate: i => Predicate(i, request), currentPage: request.PageIndex, pageSize: request.PageSize))),
            };
        }

        private static bool Predicate(Item item, GetByBrandIdItemsQueryRequest request)
        {
            bool predicate = true;

            if (request.BrandId.HasValue)
            {
                predicate = predicate && item.BrandId == request.BrandId;
            }

            return predicate;
        }
    }
}
