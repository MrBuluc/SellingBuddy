using CatalogService.Application.Bases;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetByTypeIdAndBrandId
{
    public class GetByTypeIdAndBrandIdItemQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetByTypeIdAndBrandIdItemQueryRequest, GetByTypeIdAndBrandIdItemQueryResponse>
    {
        private readonly IPictureService pictureService = pictureService;

        public async Task<GetByTypeIdAndBrandIdItemQueryResponse> Handle(GetByTypeIdAndBrandIdItemQueryRequest request, CancellationToken cancellationToken)
        {
            IReadRepository<Item> readRepository = unitOfWork.GetReadRepository<Item>();
            IList<Item> items;

            if (request.BrandId is not 0)
            {
                items = await readRepository.GetAllAsyncByPaging(cancellationToken, predicate: i => i.TypeId == request.TypeId && i.BrandId == request.BrandId && i.DeletedBy == null, currentPage: request.PageIndex, pageSize: request.PageSize);
            }
            else
            {
                items = await readRepository.GetAllAsyncByPaging(cancellationToken, predicate: i => i.TypeId == request.TypeId && i.DeletedBy == null, currentPage: request.PageIndex, pageSize: request.PageSize);
            }

            return new()
            {
                Count = items.Count,
                Items = mapper.Map<ItemDTO, Item>(pictureService.ChangeUriPlaceholder(items))
            };
        }
    }
}
