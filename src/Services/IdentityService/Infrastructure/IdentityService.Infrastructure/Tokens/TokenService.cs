using IdentityService.Application.Interfaces.Tokens;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Infrastructure.Tokens
{
    public class TokenService(IOptions<TokenSettings> options, UserManager<User> userManager) : ITokenService
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly TokenSettings tokenSettings = options.Value;

        public async Task<JwtSecurityToken> CreateToken(User user)
        {
            List<Claim> claims = [
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Name, user.Name),
            ];

            await userManager.AddClaimsAsync(user, claims);

            return new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                claims: claims,
                signingCredentials: new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)), SecurityAlgorithms.HmacSha256));
        }
    }
}
