using MediatR;

namespace IdentityService.Application.Features.Users.Command.Create
{
    public class CreateUserCommandRequest : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
