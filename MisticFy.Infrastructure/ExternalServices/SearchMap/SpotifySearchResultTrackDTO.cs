using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.Infrastructure.ExternalServices.SearchMap
{
    public class SpotifySearchResultTrackDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; } = new();
    }
}
