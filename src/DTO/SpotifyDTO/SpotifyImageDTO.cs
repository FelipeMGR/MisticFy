using System.ComponentModel.DataAnnotations;

namespace MisticFy.API.src.DTO.SpotifyDTO;
public class SpotifyImageDTO
{
    [Key]
    public string Url { get; set; }
}
