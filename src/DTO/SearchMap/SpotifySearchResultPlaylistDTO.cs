using MisticFy.API.src.DTO.SpotifyDTO;

namespace MisticFy.API.src.DTO.SearchMap
{
    public class SpotifySearchResultPlaylistDTO
    {
        public List<SpotifyPlaylistDetailsDTO> Playlists { get; set; } = new();
    }
}
