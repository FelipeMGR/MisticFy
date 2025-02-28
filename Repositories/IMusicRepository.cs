using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;

namespace MisticFy.Repositories;

public interface IMusicRepository
{
  Task<ActionResult<Playlist>> AddSongToPlaylist([FromHeader(Name = "Authorization")] string token, [FromBody] List<string> tracks, string playlistId);
}
