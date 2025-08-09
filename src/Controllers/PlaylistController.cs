using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.src.DTO.DTO;
using MisticFy.src.Repositories;

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
            SpotifyPlaylistDetailsDTO playlist = await _playlist.GetUserPlaylistAsync(userPlaylist);

            return Ok(playlist);
        }

        [HttpPost("AddSongToPlaylist")]
        [Authorize]
        public async Task<ActionResult<SpotifyPlaylistDetailsDTO>> UpdatePlaylistAsync([FromBody] AddTrackRequestDTO uris, string playlistId)
        {
            SpotifyPlaylistDetailsDTO playlist = await _playlist.AddSongToPlaylistAsync(uris, playlistId);

            return Ok(playlist);
        }

        [HttpPost("CreatePlaylist")]
        [Authorize]
        public async Task<ActionResult<SpotifyPlaylistDetailsDTO>> CreatePlaylistAsync([FromBody] Playlist playlist)
        {
            SpotifyPlaylistDetailsDTO userPlaylist = await _playlist.CreatePlaylistAsync(playlist);

            return Ok(userPlaylist);
        }
    }
}
