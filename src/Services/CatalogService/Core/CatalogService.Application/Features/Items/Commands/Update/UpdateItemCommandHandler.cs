using CatalogService.Application.Bases;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Commands.Update
{
    public class UpdateItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<UpdateItemCommandRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateItemCommandRequest request, CancellationToken cancellationToken)
        {
            Item item = await unitOfWork.GetReadRepository<Item>().GetAsync(i => i.Id == request.Id, cancellationToken) ?? throw new ItemNotFoundException(request.Id);


            Item updatedItem = mapper.Map<Item, UpdateItemCommandRequest>(request);
            updatedItem.CreatedBy = item.CreatedBy;
            await unitOfWork.GetWriteRepository<Item>().UpdateAsync(updatedItem);
            await unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
