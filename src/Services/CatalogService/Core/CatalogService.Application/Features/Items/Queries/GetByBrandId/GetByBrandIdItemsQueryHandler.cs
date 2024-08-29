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
            IList<Item> items;

            if (request.BrandId is not 0)
            {
                items = await readRepository.GetAllAsyncByPaging(cancellationToken, predicate: i => i.BrandId == request.BrandId && i.DeletedBy == null, currentPage: request.PageIndex, pageSize: request.PageSize);
            }
            else
            {
                items = await readRepository.GetAllAsyncByPaging(cancellationToken, currentPage: request.PageIndex, pageSize: request.PageSize, predicate: i => i.DeletedBy == null);
            }

            return new()
            {
                Count = items.Count,
                Items = mapper.Map<ItemDTO, Item>(pictureService.ChangeUriPlaceholder(items)),
            };
        }
    }
}
