using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;

namespace MisticFy.Repositories;

public interface IPlaylistRepository
{
  Task<ActionResult> CreatePlayListAsync([FromBody] Playlist playlist, [FromHeader(Name = "Authorization")] string token);
  Task<ActionResult<Playlist>> GetPlaylistAsync();
  Task<ActionResult> AddMusicToPlaylistAsync();
  Task<ActionResult> RemoveMusicFromPlaylistAsync();
}
