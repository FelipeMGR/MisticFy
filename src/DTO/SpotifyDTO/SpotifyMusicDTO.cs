using MisticFy.src.DTO.DTO;

namespace MisticFy.src.DTO.DTOs;

public class SpotifyMusicDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<SpotifyArtistDTO> Artists { get; set; }
    public SpotifyAlbumDTO Album { get; set; }
    public string Uri { get; set; }
}
