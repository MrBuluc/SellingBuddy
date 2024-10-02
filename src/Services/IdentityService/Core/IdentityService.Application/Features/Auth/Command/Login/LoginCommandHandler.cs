using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces.Tokens;
using IdentityService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityService.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService) : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly ITokenService tokenService = tokenService;

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);
            if (user is null || !(await userManager.CheckPasswordAsync(user, request.Password))) throw new EmailOrPasswordInvalidException();

            string token = new JwtSecurityTokenHandler().WriteToken(await tokenService.CreateToken(user));

            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", token);

            return new()
            {
                Id = user.Id,
                Token = token,
                Name = user.Name,
                Email = user.Email!
            };
        }
    }
}
