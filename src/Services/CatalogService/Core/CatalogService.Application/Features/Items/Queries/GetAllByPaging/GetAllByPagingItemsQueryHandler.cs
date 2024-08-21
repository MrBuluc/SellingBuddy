using CatalogService.Application.Bases;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetAllByPaging
{
    public class GetAllByPagingItemsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetAllByPagingItemsQueryRequest, GetAllByPagingItemsQueryResponse>
    {
        private readonly IPictureService pictureService = pictureService;

        public async Task<GetAllByPagingItemsQueryResponse> Handle(GetAllByPagingItemsQueryRequest request, CancellationToken cancellationToken)
        {
            IReadRepository<Item> readRepository = unitOfWork.GetReadRepository<Item>();
            return new()
            {
                Count = await readRepository.CountAsync(cancellationToken),
                Items = mapper.Map<IList<ItemDTO>, IList<Item>>(pictureService.ChangeUriPlaceholder(await readRepository.GetAllAsyncByPaging(cancellationToken, orderBy: queryable => queryable.OrderBy(i => i.Name), currentPage: request.PageIndex, pageSize: request.PageSize)))
            };
        }
    }
}
