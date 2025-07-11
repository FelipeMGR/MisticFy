using System.ComponentModel.DataAnnotations;

namespace MisticFy.Models;

public class Users
{
  public int Id { get; set; }
  public string SpotifyUserId { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  [Required]
  public string AccessToken { get; set; }
  public string RefreshToken { get; set; }
  public DateTime TokenExpiresAt { get; set; }
  public List<Playlist> Playlists { get; set; }
}
