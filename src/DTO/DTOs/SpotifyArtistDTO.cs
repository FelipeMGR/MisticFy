using System.ComponentModel.DataAnnotations;

namespace MisticFy.src.DTO.DTO;

public class SpotifyArtistDTO
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
}
