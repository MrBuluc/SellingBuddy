using CatalogService.Application.Bases;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetWithName
{
    public class GetWithNameItemQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetWithNameItemQueryRequest, GetWithNameItemQueryResponse>
    {
        private readonly IPictureService pictureService = pictureService;

        public async Task<GetWithNameItemQueryResponse> Handle(GetWithNameItemQueryRequest request, CancellationToken cancellationToken)
        {
            IReadRepository<Item> readRepository = unitOfWork.GetReadRepository<Item>();

            return new()
            {
                Count = await readRepository.CountAsync(cancellationToken, predicate: i => i.Name.StartsWith(request.Name)),
                Items = mapper.Map<IList<ItemDTO>, IList<Item>>(pictureService.ChangeUriPlaceholder(await readRepository.GetAllAsyncByPaging(cancellationToken, predicate: i => i.Name.StartsWith(request.Name), currentPage: request.PageIndex, pageSize: request.PageSize)))
            };
        }
    }
}
