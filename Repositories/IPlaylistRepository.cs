using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using SpotifyAPI.Web;

namespace MisticFy.Repositories;

public interface IPlaylistRepository
{
  Task<ActionResult<Playlist>> GetUserPlaylistAsync([FromHeader(Name = "Authentication")] string token, string playlistId);
  Task<ActionResult<Playlist>> CreatePlaylistAsync([FromHeader(Name = "Authentication")] string token, [FromBody] Playlist playlist);
  Task<ActionResult<Playlist>> AddSongToPlaylist([FromHeader(Name = "Authorization")] string token, [FromBody] List<string> tracks, string playlistId);

}
