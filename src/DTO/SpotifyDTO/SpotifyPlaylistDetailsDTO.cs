using SpotifyAPI.Web;

namespace MisticFy.API.src.DTO.SpotifyDTO;

public class SpotifyPlaylistDetailsDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PublicUser Owner { get; set; }
    public List<SpotifyMusicDTO> Musics { get; set; }
}