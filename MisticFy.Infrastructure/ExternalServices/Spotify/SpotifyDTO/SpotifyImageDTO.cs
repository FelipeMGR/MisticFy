using System.ComponentModel.DataAnnotations;

namespace MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;
public class SpotifyImageDTO
{
    [Key]
    public string Url { get; set; }
}
