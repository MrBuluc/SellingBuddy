using CatalogService.Application.Bases;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace CatalogService.Application.Features.Items.Queries.GetById
{
    public class GetByIdItemQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IOptionsSnapshot<CatalogSettings> optionsSnapshot) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetByIdItemQueryRequest, GetByIdItemQueryResponse>
    {
        private readonly CatalogSettings settings = optionsSnapshot.Value;

        public async Task<GetByIdItemQueryResponse> Handle(GetByIdItemQueryRequest request, CancellationToken cancellationToken)
        {
            Item? item = await unitOfWork.GetReadRepository<Item>().GetAsync(i => i.Id == request.Id && i.DeletedBy == null, cancellationToken);

            if (item is not null)
            {
                item.PictureUri = settings.PicBaseUrl + item.PictureFileName;
                return mapper.Map<GetByIdItemQueryResponse, Item>(item);
            }

            throw new ItemNotFoundException(request.Id);
        }
    }
}
