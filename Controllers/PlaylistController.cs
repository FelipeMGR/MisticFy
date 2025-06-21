using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.Repositories;
using MisticFy.Services;

namespace MisticFy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaylistController(IPlaylistRepository playlist, IUserService _userService) : ControllerBase
    {

        private readonly IPlaylistRepository _playlist = playlist;

        [HttpGet("{userPlaylist}")]
        [Authorize]
        public async Task<ActionResult> GetPlaylistAsyc([FromRoute] string userPlaylist)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userService.VerifyUser(userId);

            var playlist = await _playlist.GetUserPlaylistAsync(user.Value.AccessToken, userPlaylist);
            return Ok(playlist);

        }

        [HttpPost("{playlistId}")]
        [Authorize]
        public async Task<ActionResult<Playlist>> UpdatePlaylist([FromBody] List<string> uris, string playlistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userService.VerifyUser(userId);

            var playlist = await _playlist.AddSongToPlaylist(user.Value.AccessToken, uris, playlistId);
            return Ok(playlist);
        }

        [HttpPost("createPlaylist")]
        [Authorize]
        public async Task<ActionResult> CreatePlaylistAsync([FromBody] Playlist playlist)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userService.VerifyUser(userId);

            var userPlaylist = await _playlist.CreatePlaylistAsync(user.Value.AccessToken, playlist);

            return Ok(userPlaylist);
        }
    }
}
