using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.src.DTO;
using MisticFy.src.Repositories;
using SpotifyAPI.Web;

namespace MisticFy.src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController(ISearchRepository search) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<SpotifySearchResultDTO>> Search([FromQuery] string query, [FromQuery] SearchRequest.Types types, [FromQuery] int limit = 10)
        {
            try
            {
                SpotifySearchResultDTO result = await search.SearchAsync(query, types, limit);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
