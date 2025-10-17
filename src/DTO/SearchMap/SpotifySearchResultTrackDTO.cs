using MisticFy.API.src.DTO.SpotifyDTO;

namespace MisticFy.API.src.DTO.SearchMap
{
    public class SpotifySearchResultTrackDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; } = new();
    }
}
