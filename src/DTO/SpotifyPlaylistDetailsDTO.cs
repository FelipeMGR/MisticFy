using System;

namespace MisticFy.DTO;

public class SpotifyPlaylistDetailsDTO
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public SpotifyUserDTO Owner { get; set; }
  public List<MusicDTO> Musics { get; set; }
  public List<SpotifyImageDTO> Images { get; set; }

}
