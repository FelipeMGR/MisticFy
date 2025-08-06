using MisticFy.src.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories
{
    public interface ISearchRepository
    {
        Task<SpotifySearchResultDTO> SearchAsync(string query, SearchRequest.Types types, int limit = 10);
    }
}
