using System.ComponentModel.DataAnnotations;

namespace MisticFy.API.src.DTO.SpotifyDTO;

public class SpotifyArtistDTO
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
}
