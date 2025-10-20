using MisticFy.Domain.DTO.SpotifyDTO;
using MisticFy.Domain.Models;
using MisticFy.Domain.Repositories;

namespace MisticFy.Application.UseCases.Playlists
{
    public class PlaylistUseCase(IUnityOfWork uof)
    {
       private readonly IUnityOfWork _uof = uof;

        public async Task<SpotifyPlaylistDetailsDTO> GetUserPlaylist(string id)
        {
            return await _uof.PlaylistRepository.GetUserPlaylistAsync(id);
        }

        public async Task<SpotifyPlaylistDetailsDTO> CreatePlaylist(Playlist playlist)
        {
            return await _uof.PlaylistRepository.CreatePlaylistAsync(playlist);
        }

        public async Task<SpotifyPlaylistDetailsDTO> AddSongToPlaylist(AddTrackRequestDTO tracks, string playlistId)
        {
            return await _uof.PlaylistRepository.AddSongToPlaylistAsync(tracks, playlistId);
        }
    }
}
