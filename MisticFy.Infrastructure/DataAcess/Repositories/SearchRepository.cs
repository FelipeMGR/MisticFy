using Microsoft.AspNetCore.Http;
using MisticFy.Domain.Repositories.Search;
using MisticFy.Domain.Services.SpotifyService;
using MisticFy.Infrastructure.DataAcess.Context;
using MisticFy.Infrastructure.ExternalServices.SearchMap;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.DataAcess.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly ISpotifyService spotifyService;
        private readonly AppDbContext _context;

        public SearchRepository(IHttpContextAccessor httpContext, ISpotifyService spotifyService)
        {
            this.httpContext = httpContext;
            this.spotifyService = spotifyService;
            this._context = null!;
        }

        public SearchRepository(AppDbContext context)
        {
            this.spotifyService = null!;
            this.httpContext = null!;
            this._context = context;
        }

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
            string? accessToken = httpContext.HttpContext?.Items["SpotifyAccessToken"]?.ToString();
            SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

            var searchRequest = new SearchRequest(types, query) { Limit = limit };
            return await spotify.Search.Item(searchRequest);
        }
    }
}
