using SpotifyAPI.Web;

namespace MisticFy.API.src.Services.SpotifyServices;

public class SpotifyService() : ISpotifyService
{

    public SpotifyClient GetSpotifyClient(string token)
    {
        string accessToken = token?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();

        SpotifyClientConfig config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
        SpotifyClient spotify = new(config);

        return spotify;
    }
}
