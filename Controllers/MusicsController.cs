using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.Repositories;
using SpotifyAPI.Web;

namespace MisticFy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MusicsController(SpotifyClient spotify) : ControllerBase
    {
        private readonly SpotifyClient _spotify = spotify;

        [HttpGet("{musicName}")]
        public async Task<ActionResult> GetMusic(string musicName)
        {
            var music = await _spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, musicName));
            return Ok(music);
        }
    }
}
