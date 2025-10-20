using MisticFy.Domain.DTO.SpotifyDTO;

namespace MisticFy.Domain.Models;

public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public List<SpotifyMusicDTO> Musics { get; set; } = new List<SpotifyMusicDTO>();
}
