using System;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using SpotifyAPI.Web;

namespace MisticFy.Repositories;

public class MusicRepository : IMusicRepository
{
  public async Task<ActionResult<Playlist>> AddSongToPlaylist([FromHeader(Name = "Authorization")] string token, [FromBody] List<string> musicId, string playlistId)
  {
    var acessToken = token.Replace("Bearer :", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
    var spotify = new SpotifyClient(config);
    var updateRequest = new PlaylistAddItemsRequest(musicId);
    var result = await spotify.Playlists.AddItems(playlistId, updateRequest);

    var updatedPlaylist = await spotify.Playlists.Get(playlistId);

    return new Playlist
    {
      Id = int.Parse(updatedPlaylist.Id),
      Name = updatedPlaylist.Name,
      Description = updatedPlaylist.Description,
      Musics = updatedPlaylist.Tracks.Items.Select(m => new Music
      {
        MusicName = (m.Track as FullTrack)?.Name
      }).ToList()
    };
  }
}
