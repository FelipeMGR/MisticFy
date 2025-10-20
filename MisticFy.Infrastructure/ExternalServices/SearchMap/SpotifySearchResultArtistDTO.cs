using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.Infrastructure.ExternalServices.SearchMap
{
    public class SpotifySearchResultArtistDTO
    {
        public List<SpotifyArtistDTO> Artists { get; set; } = new();
    }
}
