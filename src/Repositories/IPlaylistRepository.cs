using Microsoft.AspNetCore.Mvc;
using MisticFy.DTO;
using MisticFy.Models;

namespace MisticFy.Repositories;

public interface IPlaylistRepository
{
  Task<ActionResult<SpotifyPlaylistDetailsDTO>> GetUserPlaylistAsync(string playlistId);
  Task<ActionResult<SpotifyPlaylistDTO>> CreatePlaylistAsync([FromBody] Playlist playlist);
  Task<ActionResult<SpotifyPlaylistDTO>> AddSongToPlaylist([FromBody] List<string> tracks, string playlistId);

}
