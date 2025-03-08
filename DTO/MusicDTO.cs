namespace MisticFy.DTO;

public class MusicDTO
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Uri { get; set; }
  public bool Explicit { get; set; }
  public List<SpotifyArtistDTO> Artists { get; set; }
  public SpotifyAlbumDTO Album { get; set; }
  public string Type { get; set; }
}
