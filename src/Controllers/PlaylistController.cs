using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.DTO;
using MisticFy.Models;
using MisticFy.Repositories;

namespace MisticFy.src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaylistController(IPlaylistRepository playlist) : ControllerBase
    {

        private readonly IPlaylistRepository _playlist = playlist;

        [HttpGet("userPlaylist")]
        [Authorize]
        public async Task<ActionResult<SpotifyPlaylistDetailsDTO>> GetPlaylistAsync(string userPlaylist)
        {
            var playlist = await _playlist.GetUserPlaylistAsync(userPlaylist);

            return Ok(playlist);
        }

        [HttpPut("AddSongToPlaylist")]
        [Authorize]
        public async Task<ActionResult<SpotifyPlaylistDTO>> UpdatePlaylistAsync([FromBody] List<string> uris, string playlistId)
        {
            var playlist = await _playlist.AddSongToPlaylist(uris, playlistId);

            return Ok(playlist);
        }

        [HttpPost("CreatePlaylist")]
        [Authorize]
        public async Task<ActionResult<SpotifyPlaylistDTO>> CreatePlaylistAsync([FromBody] Playlist playlist)
        {
            var userPlaylist = await _playlist.CreatePlaylistAsync(playlist);

            return Ok(userPlaylist);
        }
    }
}
