using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.Infrastructure.ExternalServices.SearchMap
{
    public class SpotifySearchResultPlaylistDTO
    {
        public List<SpotifyPlaylistDetailsDTO> Playlists { get; set; } = new();
    }
}
