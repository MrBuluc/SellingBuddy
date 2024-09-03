using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces.AutoMapper;
using IdentityService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Features.Users.Command.Create
{
    public class CreateUserCommandHandler(IMapper mapper, UserManager<User> userManager) : IRequestHandler<CreateUserCommandRequest, Unit>
    {
        private readonly IMapper mapper = mapper;
        private readonly UserManager<User> userManager = userManager;

        public async Task<Unit> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);
            if (user is not null) throw new UserExistsException();

            user = mapper.Map<User, CreateUserCommandRequest>(request);
            user.UserName = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.CreatedDate = DateTime.Now;

            IdentityResult result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new UserManagerCreateException(result.Errors);

            return Unit.Value;
        }
    }
}
