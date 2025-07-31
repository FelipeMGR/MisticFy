namespace MisticFy.src.DTO;

public class SpotifyPlaylistDTO
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public SpotifyUserDTO Owner { get; set; }
  public List<MusicDTO> Musics { get; set; }

}
