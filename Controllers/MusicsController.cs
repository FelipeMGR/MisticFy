using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace MisticFy.Controllers
{
    public class MusicsController : BaseApiController
    {
        public async Task<ActionResult> GetMusic(string musicName)
        {
            var spotify = new SpotifyClient("your_access_token");
            var searchResults = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, musicName));
            return Ok(searchResults);
        }
    }
}
