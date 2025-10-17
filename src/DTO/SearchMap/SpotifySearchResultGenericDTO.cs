using MisticFy.API.src.DTO.SpotifyDTO;

namespace MisticFy.API.src.DTO.SearchMap
{
    public class SpotifySearchResultGenericDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; } = new();
        public List<SpotifyAlbumDTO> Albums { get; set; } = new();
        public List<SpotifyPlaylistDetailsDTO> Playlists { get; set; } = new();
        public List<SpotifyArtistDTO> Artists { get; set; } = new();
    }
}
