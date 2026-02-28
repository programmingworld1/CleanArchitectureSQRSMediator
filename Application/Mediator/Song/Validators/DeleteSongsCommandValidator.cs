using Application.Mediator.Song.Commands;
using FluentValidation;

namespace Application.Mediator.Song.Validators
{
    public class DeleteSongsCommandValidator : AbstractValidator<DeleteSongsCommand>
    {
        public DeleteSongsCommandValidator()
        {
            RuleFor(x => x.Ids).NotEmpty();
            RuleForEach(x => x.Ids).GreaterThan(0);
        }
    }
}
