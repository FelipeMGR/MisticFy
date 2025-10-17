using MisticFy.API.src.DTO.SpotifyDTO;
using MisticFy.API.src.Models;

namespace MisticFy.API.src.Repositories;

public interface IPlaylistRepository
{
    Task<SpotifyPlaylistDetailsDTO> GetUserPlaylistAsync(string playlistId);
    Task<SpotifyPlaylistDetailsDTO> CreatePlaylistAsync(Playlist playlist);
    Task<SpotifyPlaylistDetailsDTO> AddSongToPlaylistAsync(AddTrackRequestDTO tracks, string playlistId);
}
