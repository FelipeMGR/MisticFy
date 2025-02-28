using System;

namespace MisticFy.Models;

public class Users
{
  public string? UserName { get; set; }
  public string? Password { get; set; }
  public List<Playlist>? Playlists { get; set; }
  public List<string>? LikedMusics { get; set; }
}
