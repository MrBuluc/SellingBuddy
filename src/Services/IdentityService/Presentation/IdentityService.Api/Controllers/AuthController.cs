using IdentityService.Application.Features.Auth.Command.Login;
using IdentityService.Application.Features.Users.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest request, CancellationToken cancellationToken) => StatusCode(StatusCodes.Status201Created, await mediator.Send(request, cancellationToken));

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request, CancellationToken cancellationToken) => StatusCode(StatusCodes.Status200OK, await mediator.Send(request, cancellationToken));
    }
}
