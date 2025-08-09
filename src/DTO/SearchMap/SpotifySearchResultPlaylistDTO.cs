using MisticFy.src.DTO.DTO;

namespace MisticFy.src.DTO.SearchMap
{
    public class SpotifySearchResultPlaylistDTO
    {
        public List<SpotifyPlaylistDetailsDTO> Playlists { get; set; } = new();
    }
}
