using System.ComponentModel.DataAnnotations;

namespace MisticFy.src.DTO.DTO;
public class SpotifyImageDTO
{
    [Key]
    public string Url { get; set; }
}
