using System;

namespace MisticFy.src.DTO;

public class SpotifyAlbumDTO
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Uri { get; set; }
  public List<SpotifyImageDTO> Images { get; set; }

}
