namespace MisticFy.Domain.Services.TokenServices;

public interface ISpotifyTokenRefresher
{
    Task<string> RefreshTokenAsync(string refreshToken);
}
