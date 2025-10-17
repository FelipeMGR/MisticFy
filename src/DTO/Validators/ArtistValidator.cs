using FluentValidation;
using MisticFy.API.src.DTO.SpotifyDTO;
using MisticFy.Exceptions;

namespace MisticFy.API.src.DTO.Validators
{
    public class ArtistValidator : AbstractValidator<SpotifyArtistDTO>
    {
        public ArtistValidator()
        {
            RuleFor(artist => artist.Id)
                .NotEmpty().WithMessage(ErrorMessageResource.ID_REQUIRED);
            RuleFor(artist => artist.Name)
                .NotEmpty().WithMessage(ErrorMessageResource.NAME_REQUIRED);
        }
    }
}
