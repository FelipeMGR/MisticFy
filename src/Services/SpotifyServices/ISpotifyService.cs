using SpotifyAPI.Web;

namespace MisticFy.API.src.Services.SpotifyServices;

public interface ISpotifyService
{
    SpotifyClient GetSpotifyClient(string token);
}
