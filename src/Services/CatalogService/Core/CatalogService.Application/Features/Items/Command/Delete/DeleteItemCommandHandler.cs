using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.UnitOfWorks;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Items.Command.Delete
{
    public class DeleteItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteItemCommandRequest, Unit>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteItemCommandRequest request, CancellationToken cancellationToken)
        {
            Item item = await unitOfWork.GetReadRepository<Item>().GetAsync(i => i.Id == request.Id, cancellationToken) ?? throw new ItemNotFoundException(request.Id);
            item.DeletedBy = request.DeletedBy;

            await unitOfWork.GetWriteRepository<Item>().SoftDeleteAsync(item);
            await unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        } 
    }
}
