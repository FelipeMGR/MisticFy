using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;

namespace MisticFy.Repositories;

public interface IPlaylistRepository
{
  Task<ActionResult<Playlist>> GetUserPlaylistAsync([FromHeader(Name = "Authentication")] string token, string playlistId);
  Task<ActionResult<Playlist>> CreatePlaylistAsync([FromHeader(Name = "Authentication")] string token, [FromBody] Playlist playlist);
}
