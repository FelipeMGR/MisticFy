using Microsoft.AspNetCore.Mvc;
using MisticFy.DTO;
using MisticFy.Models;

namespace MisticFy.Repositories;

public interface IPlaylistRepository
{
  Task<ActionResult<SpotifyPlaylistDTO>> GetUserPlaylistAsync([FromHeader(Name = "Authentication")] string token, string playlistId);
  Task<ActionResult<SpotifyPlaylistDTO>> CreatePlaylistAsync([FromHeader(Name = "Authentication")] string token, [FromBody] Playlist playlist);
  Task<ActionResult<SpotifyPlaylistDTO>> AddSongToPlaylist([FromHeader(Name = "Authorization")] string token, [FromBody] List<string> tracks, string playlistId);

}
