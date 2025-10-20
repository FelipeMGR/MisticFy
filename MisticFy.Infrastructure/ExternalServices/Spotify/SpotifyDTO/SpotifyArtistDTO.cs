using System.ComponentModel.DataAnnotations;

namespace MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

public class SpotifyArtistDTO
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
}
