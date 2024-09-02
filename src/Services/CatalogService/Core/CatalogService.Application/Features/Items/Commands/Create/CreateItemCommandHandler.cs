using CatalogService.Application.Bases;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Commands.Create
{
    public class CreateItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<CreateItemCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateItemCommandRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.GetWriteRepository<Item>().AddAsync(mapper.Map<Item, CreateItemCommandRequest>(request), cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
