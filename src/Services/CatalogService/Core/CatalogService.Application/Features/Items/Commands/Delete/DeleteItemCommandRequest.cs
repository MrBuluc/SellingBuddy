using MediatR;

namespace CatalogService.Application.Features.Items.Commands.Delete
{
    public class DeleteItemCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string DeletedBy { get; set; }
    }
}
