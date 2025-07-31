using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.Services;
using MisticFy.src.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories;

public class PlaylistRepository(IMapper mapper, ISpotifyService spotifyService, IHttpContextAccessor httpContextAccessor) : IPlaylistRepository
{

    public async Task<SpotifyPlaylistDTO> CreatePlaylistAsync([FromBody] Playlist playlist)
    {
        string accessToken = httpContextAccessor.HttpContext?.Items["SpotifyAccessToken"]?.ToString();

        SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

        PrivateUser currentUser = await spotify.UserProfile.Current();
        string userId = currentUser.Id;

        FullPlaylist newPlayList = await spotify.Playlists.Create(userId, new PlaylistCreateRequest(playlist.Name)
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

    public async Task<SpotifyPlaylistDetailsDTO> GetUserPlaylistAsync(string playlistId)
    {
        string accessToken = httpContextAccessor.HttpContext?.Items["SpotifyAccessToken"]?.ToString();

        SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

        FullPlaylist searchResult = await spotify.Playlists.Get(playlistId);

        return mapper.Map<SpotifyPlaylistDetailsDTO>(searchResult);
    }

    public async Task<SpotifyPlaylistDTO> AddSongToPlaylistAsync(AddTrackRequestDTO request, string playlistId)
    {
        string accessToken = httpContextAccessor.HttpContext?.Items["SpotifyAccessToken"]?.ToString();

        SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

        List<string> uris = request.Tracks.Select(track => track.Uri).ToList();

        PlaylistAddItemsRequest updateRequest = new(uris);

        _ = await spotify.Playlists.AddItems(playlistId, updateRequest);

        FullPlaylist updatedPlaylist = await spotify.Playlists.Get(playlistId);

        return MapToSpotifyPlaylist(updatedPlaylist);
    }

    private static SpotifyPlaylistDTO MapToSpotifyPlaylist(FullPlaylist playlist)
    {
        List<MusicDTO> musics = [];

        if (playlist?.Tracks?.Items != null)
        {
            foreach (var item in playlist.Tracks.Items)
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
            Name = playlist.Name,
            Owner = new SpotifyUserDTO
            {
                DisplayName = playlist.Owner.DisplayName
            },
            Musics = musics
        };
    }
}
