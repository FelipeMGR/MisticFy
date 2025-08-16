using MisticFy.src.DTO.DTOs;

namespace MisticFy.src.DTO.DTO
{
    public class AddTrackRequestDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; }
        public int? PositionToInsert { get; set; }
    }
}
