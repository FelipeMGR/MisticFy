using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.Infrastructure.ExternalServices.SearchMap
{
    public class SpotifySearchResultAlbumDTO
    {
        public List<SpotifyAlbumDTO> Albums { get; set; } = new();
    }
}
