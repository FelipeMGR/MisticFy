using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using SpotifyAPI.Web;

namespace MisticFy.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
  public Task<ActionResult> AddMusicToPlaylistAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<ActionResult> CreatePlayListAsync([FromBody] Playlist playlist, [FromHeader(Name = "Authorization")] string token)
  {
    var acessToken = token.Replace("Bearer ", "");

    var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));

    var currentUser = await spotify.UserProfile.Current();
    string userId = currentUser.Id;

    var newPlayList = await spotify.Playlists.Create(userId, new PlaylistCreateRequest(playlist.Name)
    {
      Description = playlist.Description,
      Public = playlist.IsPublic
    });

    return new OkObjectResult(newPlayList);

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
