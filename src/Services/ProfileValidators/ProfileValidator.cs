using FluentValidation.Results;
using MisticFy.API.src.DTO.SpotifyDTO;
using MisticFy.API.src.DTO.Validators;
using MisticFy.Exceptions.ExceptionsBase.AlbumErrors;
using MisticFy.Exceptions.ExceptionsBase.ArtistErrors;
using Misticy.Exceptions.ExceptionsBase.PlaylistErrors;

namespace MisticFy.API.src.Services.ProfileValidators
{
    public class ProfileValidator : IProfileValidators
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
