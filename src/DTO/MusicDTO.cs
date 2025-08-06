namespace MisticFy.src.DTO;

public class MusicDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Uri { get; set; }
    public List<SpotifyArtistDTO> Artists { get; set; }
    public SpotifyAlbumDTO Album { get; set; }
}
