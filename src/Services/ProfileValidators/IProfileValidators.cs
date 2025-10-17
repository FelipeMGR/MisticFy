using MisticFy.API.src.DTO.SpotifyDTO;

namespace MisticFy.API.src.Services.ProfileValidators
{
    public interface IProfileValidators
    {
        void Validator(SpotifyPlaylistDetailsDTO spotifyPlaylistDetailsDTO);
        void Validator(SpotifyAlbumDTO spotifyAlbumDTO);
        void Validator(SpotifyArtistDTO spotifyTrackDTO);
    }
}
