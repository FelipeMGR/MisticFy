using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MisticFy.Services;

public class TokenService : ITokenService
{
  public string GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
  {
    var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid key.");

    var privateKey = Encoding.UTF8.GetBytes(key);

    var signingCredential = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<double>("TokenValidInMinutes")),
      Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
      Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
      SigningCredentials = signingCredential
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public string GenerateRefreshToken()
  {
    var secureRandomBytes = new byte[128];

    using var randomNumberGenerator = RandomNumberGenerator.Create();

    randomNumberGenerator.GetBytes(secureRandomBytes);

    var refreshToken = Convert.ToBase64String(secureRandomBytes);

    return refreshToken;
  }

  public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)
  {
    var secretKey = _config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid key.");
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
      ValidateLifetime = true
    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);


    if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                                                         StringComparison.InvariantCultureIgnoreCase))
    {
      throw new InvalidOperationException("Invalid key");
    }
    return principal;
  }
}
