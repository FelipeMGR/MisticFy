using MisticFy.Services;
using MisticFy.src.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories
{
    public class SearchRepository(IHttpContextAccessor httpContext, ISpotifyService spotifyService) : ISearchRepository
    {
        public async Task<SpotifySearchResultDTO> SearchAsync(string query, SearchRequest.Types types, int limit = 10)
        {
            string accessToken = httpContext.HttpContext?.Items["SpotifyAccessToken"].ToString();

            SpotifySearchResultDTO result = await spotifyService.SearchAsync(accessToken, query, types, limit);

            return result;

        }
    }
}
