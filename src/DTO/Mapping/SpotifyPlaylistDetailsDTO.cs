using MisticFy.src.DTO.DTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.DTO;

public class SpotifyPlaylistDetailsDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SpotifyUserDTO Owner { get; set; }
    public IEnumerable<SpotifyMusicDTO> Musics { get; set; }
}