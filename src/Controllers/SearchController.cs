using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.src.DTO.DTOs;
using MisticFy.src.DTO.SearchMap;
using MisticFy.src.Repositories;
using SpotifyAPI.Web;

namespace MisticFy.src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController(ISearchRepository search) : ControllerBase
    {
        [HttpGet("search-track")]
        [Authorize]
        public async Task<ActionResult<SpotifySearchResultTrackDTO>> SearchTrack([FromQuery] string query, [FromQuery] SearchRequest.Types types = SearchRequest.Types.Track, [FromQuery] int limit = 10)
        {
            try
            {
                SpotifySearchResultTrackDTO result = await search.SearchTrackAsync(query, types, limit);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
