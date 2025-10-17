using FluentValidation;
using MisticFy.API.src.DTO.SpotifyDTO;
using MisticFy.Exceptions;

namespace MisticFy.API.src.DTO.Validators
{
    public class AlbumValidator : AbstractValidator<SpotifyAlbumDTO>
    {
        public AlbumValidator()
        {
            RuleFor(album => album.Id)
                .NotEmpty().WithMessage(ErrorMessageResource.ID_REQUIRED);

            RuleFor(album => album.Name)
                .NotEmpty().WithMessage(ErrorMessageResource.NAME_REQUIRED);

            RuleFor(album => album.ReleaseDate)
                .NotEmpty().WithMessage(ErrorMessageResource.RELEASE_DATE_REQUIRED);
        }
    }
}
