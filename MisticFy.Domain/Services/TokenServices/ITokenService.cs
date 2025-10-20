using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace MisticFy.Domain.Services.TokenServices;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
}
