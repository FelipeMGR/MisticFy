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

        [HttpGet("{userPlaylist}")]
        public async Task<ActionResult> GetPlaylistAsyc([FromHeader(Name = "Authorization")] string token, [FromRoute] string userPlaylist)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userPlaylist))
            {
                return BadRequest("Token/Playlist is required");
            }

            var playlist = await _playlist.GetUserPlaylistAsync(token, userPlaylist);
            return Ok(playlist);
        }

        [HttpPost("{playlistId}")]
        public async Task<ActionResult<Playlist>> UpdatePlaylist([FromHeader(Name = "Authorization")] string token, [FromBody] List<string> uris, string playlistId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(playlistId))
            {
                return BadRequest("Token/Playlist is required");
            }

            var playlist = await _playlist.AddSongToPlaylist(token, uris, playlistId);

            return Ok(playlist);
        }

        [HttpPost("createPlaylist")]
        public async Task<ActionResult> CreatePlaylistAsync([FromHeader(Name = "Authorization")] string token, [FromBody] Playlist playlist)
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
