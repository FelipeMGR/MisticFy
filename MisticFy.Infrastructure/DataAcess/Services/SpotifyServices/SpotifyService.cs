using MisticFy.Domain.Services.SpotifyService;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.DataAcess.Services.SpotifyServices;

internal class SpotifyService() : ISpotifyService
{

    public SpotifyClient GetSpotifyClient(string token)
    {
        string accessToken = token?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();

        SpotifyClientConfig config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
        SpotifyClient spotify = new(config);

        return spotify;
    }
}
