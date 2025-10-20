using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Domain.Models;
using MisticFy.Domain.Repositories.Playlists;
using MisticFy.Domain.Services.SpotifyService;
using MisticFy.Infrastructure.DataAcess.Context;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.DataAcess.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly IMapper mapper;
    private readonly ISpotifyService spotifyService;
    private readonly IHttpContextAccessor httpContextAccessor;
    private AppDbContext _context;

    public PlaylistRepository(IMapper mapper, ISpotifyService spotifyService, IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        this.mapper = mapper;
        this.spotifyService = spotifyService;
        this.httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    // Corrigido: inicializa os campos não anuláveis com valores padrão nulos
    public PlaylistRepository(AppDbContext context)
    {
        _context = context;
        mapper = null!;
        spotifyService = null!;
        httpContextAccessor = null!;
    }

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

    public async Task<SpotifyPlaylistDetailsDTO> AddSongToPlaylistAsync(AddTrackRequest request, string playlistId)
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
