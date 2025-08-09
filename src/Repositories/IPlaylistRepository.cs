using MisticFy.Models;
using MisticFy.src.DTO.DTO;

namespace MisticFy.src.Repositories;

public interface IPlaylistRepository
{
    Task<SpotifyPlaylistDetailsDTO> GetUserPlaylistAsync(string playlistId);
    Task<SpotifyPlaylistDetailsDTO> CreatePlaylistAsync(Playlist playlist);
    Task<SpotifyPlaylistDetailsDTO> AddSongToPlaylistAsync(AddTrackRequestDTO tracks, string playlistId);
}
