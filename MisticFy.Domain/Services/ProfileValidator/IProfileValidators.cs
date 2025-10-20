using MisticFy.Domain.DTO.SpotifyDTO;

namespace MisticFy.Domain.Services.ProfileValidator
{
    public interface IProfileValidators
    {
        void Validator(SpotifyPlaylistDetailsDTO spotifyPlaylistDetailsDTO);
        void Validator(SpotifyAlbumDTO spotifyAlbumDTO);
        void Validator(SpotifyArtistDTO spotifyTrackDTO);
    }
}
