using FluentValidation.Results;
using MisticFy.Domain.DTO.Validators;
using MisticFy.Domain.Services.ProfileValidator;
using MisticFy.Exceptions.ExceptionsBase.AlbumErrors;
using MisticFy.Exceptions.ExceptionsBase.ArtistErrors;
using MisticFy.Exceptions.ExceptionsBase.PlaylistErrors;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;

namespace MisticFy.Infrastructure.DataAcess.Services.ProfileValidators
{
    internal class ProfileValidator : IProfileValidators
    {
        public void Validator(SpotifyPlaylistDetailsDTO spotifyPlaylistDetailsDTO)
        {
            PlaylistValidator playlistValid = new();

            ValidationResult result = playlistValid.Validate(spotifyPlaylistDetailsDTO);

            if (!result.IsValid)
            {

                List<string> errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnPlaylistSearch(errors);
            }
        }

        public void Validator(SpotifyAlbumDTO spotifyAlbumDTO)
        {
            AlbumValidator albumValid = new();

            ValidationResult result = albumValid.Validate(spotifyAlbumDTO);

            if (!result.IsValid)
            {
                List<string> errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnAlbumSearch(errors);
            }
        }

        public void Validator(SpotifyArtistDTO spotifyTrackDTO)
        {
            ArtistValidator artistValid = new();
            ValidationResult result = artistValid.Validate(spotifyTrackDTO);
            if (!result.IsValid)
            {
                List<string> errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnArtistSearch(errors);
            }
        }
    }
}
