using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.Infrastructure.ExternalServices.SearchMap
{
    public class SpotifySearchResultGenericDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; } = new();
        public List<SpotifyAlbumDTO> Albums { get; set; } = new();
        public List<SpotifyPlaylistDetailsDTO> Playlists { get; set; } = new();
        public List<SpotifyArtistDTO> Artists { get; set; } = new();
    }
}
