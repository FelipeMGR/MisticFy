namespace MisticFy.src.DTO;

public class SpotifySearchResultDTO
{
  public SpotifyPagingDTO<SpotifyArtistDTO> Artists { get; set; }
  public SpotifyPagingDTO<MusicDTO> Tracks { get; set; }
  public SpotifyPagingDTO<SpotifyPlaylistDTO> Playlists { get; set; }
  public SpotifyPagingDTO<SpotifyAlbumDTO> Albums { get; set; }
}
