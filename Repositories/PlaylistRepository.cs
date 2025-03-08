using Microsoft.AspNetCore.Mvc;
using MisticFy.DTO;
using MisticFy.Models;
using SpotifyAPI.Web;

namespace MisticFy.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
  public async Task<ActionResult<SpotifyPlaylistDTO>> CreatePlaylistAsync(string token, [FromBody] Playlist playlist)
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

    return new SpotifyPlaylistDTO
    {
      Name = newPlayList.Name,
      Description = newPlayList.Description,
      Owner = new SpotifyUserDTO
      {
        DisplayName = newPlayList.Owner.DisplayName
      },
      Musics = []
    };
  }

  public async Task<ActionResult<SpotifyPlaylistDTO>> GetUserPlaylistAsync(string token, string playlistId)
  {
    var accessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
    var spotify = new SpotifyClient(config);

    var playlist = await spotify.Playlists.Get(playlistId);

    List<MusicDTO> playlistMusics = new List<MusicDTO>();

    if (playlist?.Tracks?.Items != null)
    {
      foreach (var item in playlist.Tracks.Items)
      {
        if (item.Track is FullTrack track)
        {
          playlistMusics.Add(new MusicDTO
          {
            Name = track.Name,
            Artists = track.Artists.Select(a => new SpotifyArtistDTO
            {
              Name = a.Name
            }).ToList(),
            Album = new SpotifyAlbumDTO
            {
              Name = track.Album.Name,
              Images = track.Album.Images.Select(i => new SpotifyImageDTO
              {
                Url = i.Url
              }).ToList()
            }
          });
        }
      }
    }

    return new SpotifyPlaylistDTO
    {
      Name = playlist?.Name,
      Description = playlist?.Description,
      Owner = new SpotifyUserDTO
      {
        DisplayName = playlist?.Owner.DisplayName
      },
      Musics = playlistMusics.Select(m => new MusicDTO
      {
        Name = m.Name,
        Artists = m.Artists.Select(a => new SpotifyArtistDTO { Name = a.Name }).ToList(),
        Album = new SpotifyAlbumDTO
        {
          Name = m.Album.Name,
          Images = m.Album.Images.Select(i => new SpotifyImageDTO { Url = i.Url }).ToList()
        }
      }).ToList()
    };
  }

  public async Task<ActionResult<SpotifyPlaylistDTO>> AddSongToPlaylist(string token, [FromBody] List<string> uris, string playlistId)
  {
    var acessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
    var spotify = new SpotifyClient(config);
    var updateRequest = new PlaylistAddItemsRequest(uris);

    var result = await spotify.Playlists.AddItems(playlistId, updateRequest);

    var updatedPlaylist = await spotify.Playlists.Get(playlistId);

    var musics = new List<MusicDTO>();

    if (updatedPlaylist?.Tracks?.Items != null)
    {
      foreach (var item in updatedPlaylist.Tracks.Items)
      {
        if (item.Track is FullTrack track)
        {
          musics.Add(new MusicDTO
          {
            Name = track.Name,
            Artists = track.Artists.Select(a => new SpotifyArtistDTO
            {
              Name = a.Name
            }).ToList(),
            Album = new SpotifyAlbumDTO
            {
              Name = track.Album.Name,
              Images = track.Album.Images.Select(i => new SpotifyImageDTO
              {
                Url = i.Url
              }).ToList()
            }
          });
        }
      }
    }
    return new SpotifyPlaylistDTO
    {
      Name = updatedPlaylist.Name,
      Owner = new SpotifyUserDTO
      {
        DisplayName = updatedPlaylist.Owner.DisplayName
      },
      Musics = musics
    };
  }
}
