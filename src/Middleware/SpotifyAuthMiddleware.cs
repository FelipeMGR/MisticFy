using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MisticFy.Context;
using MisticFy.src.Models;
using MisticFy.src.Services;

namespace MisticFy.src.Middleware;

public class SpotifyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, AppDbContext db, ISpotifyTokenRefresher tokenRefresher)
    {

        if (!context.Request.Cookies.TryGetValue("SessionToken", out string jwtToken))
        {
            await _next(context);
            return;
        }


        if (context.Request.Cookies.TryGetValue("SessionToken", out jwtToken))
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);

                ClaimsPrincipal principal = tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                context.User = principal;
            }
            catch
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token not valid or expired.");
                return;
            }
        }

        string userStrId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userStrId, out int userId))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid user");
            return;
        }

        Users user = await db.Users.FindAsync(userId);
        if (user == null)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("User not found");
            return;
        }

        if (user.TokenExpiresAt < DateTime.UtcNow)
        {
            try
            {
                var refreshToken = await tokenRefresher.RefreshTokenAsync(user.RefreshToken);

                if (string.IsNullOrEmpty(refreshToken))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token, you need to log in again");
                    return;
                }

                user.AccessToken = await tokenRefresher.RefreshTokenAsync(user.RefreshToken);
                user.TokenExpiresAt = DateTime.UtcNow.AddMinutes(15);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"Error while renovating token. You'll need to login again. {ex.Message}");
                return;

            }
        }

        context.Items["SpotifyAccessToken"] = user.AccessToken;

        await _next(context);
    }
}
