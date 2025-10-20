using Microsoft.Extensions.Configuration;
using MisticFy.Domain.Services.TokenServices;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.DataAcess.Services.TokenServices;

internal class SpotifyTokenRefresher(IConfiguration _config) : ISpotifyTokenRefresher
{
    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var clientId = _config["Spotify:ClientId"];
        var clientSecret = _config["Spotify:ClientSecret"];

        var response = await new OAuthClient().RequestToken(new AuthorizationCodeRefreshRequest(clientId, clientSecret, refreshToken));

        return response.AccessToken;
    }
}
