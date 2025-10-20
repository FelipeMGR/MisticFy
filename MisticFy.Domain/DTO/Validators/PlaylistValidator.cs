using FluentValidation;
using MisticFy.Domain.DTO.SpotifyDTO;
using MisticFy.Exceptions;

namespace MisticFy.Domain.DTO.Validators
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
