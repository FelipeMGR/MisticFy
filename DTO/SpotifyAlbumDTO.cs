using System;

namespace MisticFy.DTO;

public class SpotifyAlbumDTO
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Uri { get; set; }
  public List<SpotifyImageDTO> Images { get; set; }
  public string ReleaseDate { get; set; }
  public string Type { get; set; }

}
