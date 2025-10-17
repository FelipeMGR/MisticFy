using FluentValidation;
using MisticFy.API.src.DTO.SpotifyDTO;
using MisticFy.Exceptions;

namespace MisticFy.API.src.DTO.Validators
{
    public class PlaylistValidator : AbstractValidator<SpotifyPlaylistDetailsDTO>
    {   public PlaylistValidator()
        {
            RuleFor(playlist => playlist.Id)
                .NotEmpty().WithMessage(ErrorMessageResource.NAME_REQUIRED);
            RuleFor(playlist => playlist.Name)
                .NotEmpty().WithMessage(ErrorMessageResource.PLAYLIST_NAME_REQUIRED);
            RuleFor(playlist => playlist.Description)
                .MaximumLength(300).WithMessage(ErrorMessageResource.PLAYLIST_DESCRIPTION);
            RuleFor(playlist => playlist.Owner)
                .NotNull().WithMessage(ErrorMessageResource.PLAYLIST_OWNER);
        }
    }
}
