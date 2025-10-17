namespace MisticFy.API.src.DTO.SpotifyDTO
{
    public class AddTrackRequestDTO
    {
        public List<SpotifyMusicDTO> Tracks { get; set; }
        public int? PositionToInsert { get; set; }
    }
}
