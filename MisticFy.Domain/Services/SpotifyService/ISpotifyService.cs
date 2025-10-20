using SpotifyAPI.Web;

namespace MisticFy.Domain.Services.SpotifyService;

public interface ISpotifyService
{
    SpotifyClient GetSpotifyClient(string token);
}
