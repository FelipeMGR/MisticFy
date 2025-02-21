using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;

namespace MisticFy.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
  public Task<ActionResult> AddMusicToPlaylistAsync()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult> CreatePlayListAsync()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<Playlist>> GetPlaylistAsync()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult> RemoveMusicFromPlaylistAsync()
  {
    throw new NotImplementedException();
  }
}
