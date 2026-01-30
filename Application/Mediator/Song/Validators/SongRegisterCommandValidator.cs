using Application.Mediator.Song.Commands;
using FluentValidation;

namespace Application.Mediator.Song.Validators
{
    public class SongRegisterCommandValidator : AbstractValidator<CreateSongCommand>
    {
        public SongRegisterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ArtistName).NotEmpty();
        }
    }
}
