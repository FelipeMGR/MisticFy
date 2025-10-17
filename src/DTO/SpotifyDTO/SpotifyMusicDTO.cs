namespace MisticFy.API.src.DTO.SpotifyDTO;

public class SpotifyMusicDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<SpotifyArtistDTO> Artists { get; set; }
    public SpotifyAlbumDTO Album { get; set; }
    public string Uri { get; set; }
}
