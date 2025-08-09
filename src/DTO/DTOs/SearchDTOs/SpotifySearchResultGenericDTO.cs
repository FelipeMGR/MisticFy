using MisticFy.src.DTO.DTO;

namespace MisticFy.src.DTO.DTOs.SearchDTOs
{
    public class SpotifySearchResultGenericDTO
    {
        public SpotifyAlbumDTO Albums { get; set; }
        public SpotifyPlaylistDetailsDTO Playlists { get; set; }
        public SpotifyArtistDTO Artists { get; set; }
        public SpotifyMusicDTO Tracks { get; set; }

    }
}
