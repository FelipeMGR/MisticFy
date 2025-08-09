using MisticFy.Services;
using MisticFy.src.DTO.SearchMap;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories
{
    public class SearchRepository(IHttpContextAccessor httpContext, ISpotifyService spotifyService) : ISearchRepository
    {
        public async Task<SpotifySearchResultAlbumDTO> SearchAlbumAsync(string query, SearchRequest.Types types = SearchRequest.Types.Album, int limit = 10)
        {
            var result = await SearchRawAsync(query, types, limit);
            return SearchMapper.MapToAlbumDTO(result);
        }

        public async Task<SpotifySearchResultArtistDTO> SearchArtistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Artist, int limit = 10)
        {
            var result = await SearchRawAsync(query, types, limit);
            return SearchMapper.MapToArtistDTO(result);
        }

        public async Task<SpotifySearchResultPlaylistDTO> SearchPlaylistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Playlist, int limit = 10)
        {
            var result = await SearchRawAsync(query, types, limit);
            return SearchMapper.MapToPlaylistDTO(result);
        }

        public async Task<SpotifySearchResultTrackDTO> SearchTrackAsync(string query, SearchRequest.Types types = SearchRequest.Types.Track, int limit = 10)
        {
            SearchResponse result = await SearchRawAsync(query, types, limit);
            return SearchMapper.MapToTrackDTO(result);
        }

        private async Task<SearchResponse> SearchRawAsync(string query, SearchRequest.Types types, int limit)
        {
            string accessToken = httpContext.HttpContext?.Items["SpotifyAccessToken"]?.ToString();
            var spotify = spotifyService.GetSpotifyClient(accessToken);

            var searchRequest = new SearchRequest(types, query) { Limit = limit };
            return await spotify.Search.Item(searchRequest);
        }
    }
}
