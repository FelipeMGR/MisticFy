using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.Repositories;
using MisticFy.Services;
using SpotifyAPI.Web;

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
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null || string.IsNullOrEmpty(user.AccessToken))
            {
                return Unauthorized("User not found or access token missing.");
            }

            var playlist = await _playlist.GetUserPlaylistAsync(user.AccessToken, userPlaylist);
            return Ok(playlist);

        }

        [HttpPost("{playlistId}")]
        [Authorize]
        public async Task<ActionResult<Playlist>> UpdatePlaylist([FromBody] List<string> uris, string playlistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null || string.IsNullOrEmpty(user.AccessToken))
            {
                return Unauthorized("User not found or access token missing.");
            }

            var playlist = await _playlist.AddSongToPlaylist(user.AccessToken, uris, playlistId);
            return Ok(playlist);
        }

        [HttpPost("createPlaylist")]
        [Authorize]
        public async Task<ActionResult> CreatePlaylistAsync([FromBody] Playlist playlist)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null || string.IsNullOrEmpty(user.AccessToken))
            {
                return Unauthorized("User not found or access token missing.");
            }

            var userPlaylist = await _playlist.CreatePlaylistAsync(user.AccessToken, playlist);

            return Ok(userPlaylist);
        }
    }
}
