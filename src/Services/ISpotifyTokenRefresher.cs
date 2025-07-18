namespace MisticFy.src.Services;

public interface ISpotifyTokenRefresher
{
  Task<string> RefreshTokenAsync(string refreshToken);
}
