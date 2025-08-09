using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using MisticFy.Services;
using MisticFy.src.DTO;
using MisticFy.src.DTO.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories;

public class PlaylistRepository(IMapper mapper, ISpotifyService spotifyService, IHttpContextAccessor httpContextAccessor) : IPlaylistRepository
{

    public async Task<SpotifyPlaylistDetailsDTO> CreatePlaylistAsync([FromBody] Playlist playlist)
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

        return mapper.Map<SpotifyPlaylistDetailsDTO>(newPlayList);
    }

    public async Task<SpotifyPlaylistDetailsDTO> GetUserPlaylistAsync(string playlistId)
    {
        string accessToken = httpContextAccessor.HttpContext?.Items["SpotifyAccessToken"]?.ToString();

        SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

        FullPlaylist searchResult = await spotify.Playlists.Get(playlistId);

        return mapper.Map<SpotifyPlaylistDetailsDTO>(searchResult);
    }

    public async Task<SpotifyPlaylistDetailsDTO> AddSongToPlaylistAsync(AddTrackRequestDTO request, string playlistId)
    {
        string accessToken = httpContextAccessor.HttpContext?.Items["SpotifyAccessToken"]?.ToString();

        SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

        List<string> uris = request.Tracks.Select(track => track.Uri).ToList();

        PlaylistAddItemsRequest updateRequest = new(uris);

        _ = await spotify.Playlists.AddItems(playlistId, updateRequest);

        FullPlaylist updatedPlaylist = await spotify.Playlists.Get(playlistId);

        return mapper.Map<SpotifyPlaylistDetailsDTO>(updatedPlaylist);
    }
}
