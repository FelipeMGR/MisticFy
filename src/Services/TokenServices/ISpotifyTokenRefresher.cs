namespace MisticFy.API.src.Services.TokenServices;

public interface ISpotifyTokenRefresher
{
    Task<string> RefreshTokenAsync(string refreshToken);
}
