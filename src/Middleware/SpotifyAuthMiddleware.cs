using System.Security.Claims;
using MisticFy.Context;
using MisticFy.src.Services;
using SpotifyAPI.Web;

namespace MisticFy.src.Middleware;

public class SpotifyAuthMiddleware(RequestDelegate next)
{
  private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, AppDbContext db, ISpotifyTokenRefresher tokenRefresher)
  {
    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId != null)
    {
      var user = await db.Users.FindAsync(int.Parse(userId));

      if (user != null)
      {
        if (user.TokenExpiresAt < DateTime.UtcNow)
        {
          user.AccessToken = await tokenRefresher.RefreshTokenAsync(user.RefreshToken);
          user.TokenExpiresAt = DateTime.UtcNow.AddSeconds(3600);
          await db.SaveChangesAsync();
        }

         context.Items["SpotifyAccessToken"] = user?.AccessToken;
      }
    }

    await _next(context);
  }
}

