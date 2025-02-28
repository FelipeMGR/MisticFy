using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using SpotifyAPI.Web;

namespace MisticFy.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
  public async Task<ActionResult<Playlist>> CreatePlaylistAsync([FromHeader(Name = "Authorization")] string token, [FromBody] Playlist playlist)
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

    return new Playlist
    {
      Name = newPlayList.Name,
      Description = newPlayList.Description,
      Musics = []
    };
  }

  public async Task<ActionResult<Playlist>> GetUserPlaylistAsync([FromHeader(Name = "Authorization")] string token, string playlistId)
  {
    var acessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
    var client = new SpotifyClient(config);

    var playlist = await client.Playlists.Get(playlistId);

    List<Music> playlistMusics = new List<Music>();

    if (playlist.Tracks.Items != null)
    {
      foreach (var item in playlist.Tracks.Items)
      {
        var track = item.Track as FullTrack;
        if (track != null)
        {
          playlistMusics.Add(new Music
          {
            MusicName = track.Name
          });
        }
      }
    }

    return new Playlist
    {
      //Id = int.Parse(playlist.Id),
      Name = playlist.Name,
      Description = playlist.Description,
      Musics = playlistMusics
    };
  }

  public async Task<ActionResult<Playlist>> AddSongToPlaylist([FromHeader(Name = "Authorization")] string token, [FromBody] List<string> uris, string playlistId)
  {
    var acessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
    var spotify = new SpotifyClient(config);
    var updateRequest = new PlaylistAddItemsRequest(uris);

    var result = await spotify.Playlists.AddItems(playlistId, updateRequest);

    await Task.Delay(1000);

    var updatedPlaylist = await spotify.Playlists.Get(playlistId);

    var musics = updatedPlaylist.Tracks.Items
                    .Where(item => item.Track is FullTrack)
                    .Select(m => new Music
                    {
                      MusicName = ((FullTrack)m.Track).Name
                    })
                    .ToList();


    return new Playlist
    {
      Name = updatedPlaylist.Name,
      Musics = musics
    };
  }
}
