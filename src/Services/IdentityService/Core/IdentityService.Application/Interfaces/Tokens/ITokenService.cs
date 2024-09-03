using IdentityService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityService.Application.Interfaces.Tokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user);
    }
}
