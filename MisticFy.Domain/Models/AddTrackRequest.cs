namespace MisticFy.Domain.Models
{
    public class AddTrackRequest
    {
        public List<Music> Tracks { get; set; }
        public int? PositionToInsert { get; set; }
    }
}
