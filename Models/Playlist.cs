using System;

namespace MisticFy.Models;

public class Playlist
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public bool IsPublic { get; set; }
  public List<Music> Musics { get; set; }
}
