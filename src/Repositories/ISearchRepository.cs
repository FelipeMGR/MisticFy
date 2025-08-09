using MisticFy.src.DTO.DTOs.SearchDTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories
{
    public interface ISearchRepository
    {
        SpotifySearchResultTrackDTO SearchTrackAsync(string query, SearchRequest.Types types = SearchRequest.Types.Track, int limit = 10);
        SpotifySearchResultAlbumDTO SearchAlbumAsync(string query, SearchRequest.Types types = SearchRequest.Types.Album, int limit = 10);
        SpotifySearchResultArtistDTO SearchArtistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Artist, int limit = 10);
        SpotifySearchResultPlaylistDTO SearchPlaylistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Playlist, int limit = 10);
    }
}
