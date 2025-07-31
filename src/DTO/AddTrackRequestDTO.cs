namespace MisticFy.src.DTO
{
    public class AddTrackRequestDTO
    {
        public List<MusicDTO> Tracks { get; set; }
        public int? PositionToInsert { get; set; }
    }
}
