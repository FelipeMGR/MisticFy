using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Application.UseCases.Playlists;
using MisticFy.Domain.Models;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.API.src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaylistController(PlaylistUseCase playlist) : ControllerBase
    {

        private readonly PlaylistUseCase _playlist = playlist;

        [HttpGet("userPlaylist")]
        [Authorize]
        public async Task<IActionResult> GetPlaylistAsync(string userPlaylist)
        {
            var playlist = await _playlist.GetUserPlaylist(userPlaylist);

            return Ok(playlist);
        }

        [HttpPost("AddSongToPlaylist")]
        [Authorize]
        public async Task<IActionResult> UpdatePlaylistAsync([FromBody] AddTrackRequest uris, string playlistId)
        {
            var playlist = await _playlist.AddSongToPlaylist(uris, playlistId);

            return Ok(playlist);
        }

        [HttpPost("CreatePlaylist")]
        [Authorize]
        public async Task<IActionResult> CreatePlaylistAsync([FromBody] Playlist playlist)
        {
            var userPlaylist = await _playlist.CreatePlaylist(playlist);

            return Ok(userPlaylist);
        }
    }
}
