using MisticFy.Domain.Models;

namespace MisticFy.Domain.Repositories.Playlists;

public interface IPlaylistRepository
{
    Task<Playlist> GetUserPlaylistAsync(string playlistId);
    Task<Playlist> CreatePlaylistAsync(Playlist playlist);
    Task<Playlist> AddSongToPlaylistAsync(AddTrackRequest tracks, string playlistId);
}
