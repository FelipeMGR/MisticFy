using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("album/{albumId}")]
        public async Task<ActionResult> GetSingleAlbum(string albumId)
        {
            var album = await _spotify.Albums.Get(albumId);

            if (album == null)
            {
                return NotFound("Album not found. Check the album ID and try again.");
            }
            return Ok(album);
        }
    }
}
