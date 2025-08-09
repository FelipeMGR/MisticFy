using MisticFy.src.DTO;
using SpotifyAPI.Web;

namespace MisticFy.Services;

public interface ISpotifyService
{
    SpotifyClient GetSpotifyClient(string token);
}
