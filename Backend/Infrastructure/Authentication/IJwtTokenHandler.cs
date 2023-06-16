using System.IdentityModel.Tokens.Jwt;
using Topshiriq.Domain.Entities.Users;

namespace Topshiriq.Infrastructure.Authentication;

public interface IJwtTokenHandler
{
    JwtSecurityToken GenerateAccessToken(User user);
}