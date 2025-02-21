using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;

namespace MisticFy.Repositories;

public interface IPlaylistRepository
{
  Task<ActionResult> CreatePlayListAsync();
  Task<ActionResult<Playlist>> GetPlaylistAsync();
  Task<ActionResult> AddMusicToPlaylistAsync();
  Task<ActionResult> RemoveMusicFromPlaylistAsync();
}
