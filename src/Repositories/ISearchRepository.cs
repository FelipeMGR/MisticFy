using MisticFy.API.src.DTO.SearchMap;
using SpotifyAPI.Web;

namespace MisticFy.API.src.Repositories
{
    public interface ISearchRepository
    {
        Task<SpotifySearchResultTrackDTO> SearchTrackAsync(string query, SearchRequest.Types types = SearchRequest.Types.Track, int limit = 10);
        Task<SpotifySearchResultAlbumDTO> SearchAlbumAsync(string query, SearchRequest.Types types = SearchRequest.Types.Album, int limit = 10);
        Task<SpotifySearchResultArtistDTO> SearchArtistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Artist, int limit = 10);
        Task<SpotifySearchResultPlaylistDTO> SearchPlaylistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Playlist, int limit = 10);
    }
}
