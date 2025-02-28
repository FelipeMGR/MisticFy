using System;

namespace MisticFy.Services;

public interface ISpotifyTokenRefresher
{
  Task<string> RefreshTokenAsync(string refreshToken);
}
