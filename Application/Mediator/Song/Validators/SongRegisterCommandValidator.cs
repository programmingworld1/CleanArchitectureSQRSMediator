using Application.Mediator.Song.Commands;
using FluentValidation;

namespace Application.Mediator.Song.Validators
{
    public class SongRegisterCommandValidator : AbstractValidator<SongRegisterCommand>
    {
        public SongRegisterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ArtistName).NotEmpty();
        }
    }
}
