using System;
using System.Security.Claims;
using MisticFy.Context;
using MisticFy.Services;
using SpotifyAPI.Web;

namespace MisticFy.Middleware;

public class SpotifyAuthMiddleware
{
  private readonly RequestDelegate _next;

  public SpotifyAuthMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context, AppDbContext db, SpotifyTokenRefresher tokenRefresher)
  {
    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId != null)
    {
      var user = await db.Users.FindAsync(int.Parse(userId));

      if (user != null)
      {
        // Refresh token if expired
        if (user.TokenExpiresAt < DateTime.UtcNow)
        {
          user.AccessToken = await tokenRefresher.RefreshTokenAsync(user.RefreshToken);
          user.TokenExpiresAt = DateTime.UtcNow.AddSeconds(3600); // 1 hour
          await db.SaveChangesAsync();
        }

        // Attach Spotify client to the request context
        var spotify = new SpotifyClient(user.AccessToken);
        context.Items["SpotifyClient"] = spotify;
      }
    }

    await _next(context);
  }
}

