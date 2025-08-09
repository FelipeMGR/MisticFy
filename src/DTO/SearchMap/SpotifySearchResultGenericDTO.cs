using MisticFy.src.DTO.DTO;
using MisticFy.src.DTO.DTOs;

namespace MisticFy.src.DTO.SearchMap
{
    public class SpotifySearchResultGenericDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; } = new();
        public List<SpotifyAlbumDTO> Albums { get; set; } = new();
        public List<SpotifyPlaylistDetailsDTO> Playlists { get; set; } = new();
        public List<SpotifyArtistDTO> Artists { get; set; } = new();
    }
}
