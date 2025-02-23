using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.Repositories;
using SpotifyAPI.Web;

namespace MisticFy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaylistController(IPlaylistRepository playlist) : ControllerBase
    {

        private readonly IPlaylistRepository _playlist = playlist;

        [HttpGet("{playlistId}")]
        public async Task<ActionResult> GetPlaylistAsyc([FromHeader(Name = "Authentication")] string token, [FromRoute] string playlistId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(playlistId))
            {
                return BadRequest("Token/Playlist is required");
            }

            var userPlaylist = await _playlist.GetUserPlaylistAsync(token, playlistId);
            return Ok(userPlaylist);
        }

        [HttpPost("createPlaylist")]
        public async Task<ActionResult> CreatePlaylistAsync([FromBody] Playlist playlist, [FromHeader(Name = "Authentication")] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Authentication token is required");
            }
            else if (playlist == null)
            {
                return BadRequest("Please, enter the playlist info");
            }

            var userPlaylist = await _playlist.CreatePlaylistAsync(token, playlist);

            return Ok(userPlaylist);
        }
    }
}
