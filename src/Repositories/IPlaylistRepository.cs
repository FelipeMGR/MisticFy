using MisticFy.Models;
using MisticFy.src.DTO;

namespace MisticFy.src.Repositories;

public interface IPlaylistRepository
{
    Task<SpotifyPlaylistDetailsDTO> GetUserPlaylistAsync(string playlistId);
    Task<SpotifyPlaylistDTO> CreatePlaylistAsync(Playlist playlist);
    Task<SpotifyPlaylistDTO> AddSongToPlaylistAsync(AddTrackRequestDTO tracks, string playlistId);
}
