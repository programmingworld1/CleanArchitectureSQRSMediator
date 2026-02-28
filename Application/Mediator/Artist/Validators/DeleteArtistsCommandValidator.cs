using Application.Mediator.Artist.Commands;
using FluentValidation;

namespace Application.Mediator.Artist.Validators
{
    public class DeleteArtistsCommandValidator : AbstractValidator<DeleteArtistsCommand>
    {
        public DeleteArtistsCommandValidator()
        {
            RuleFor(x => x.Ids).NotEmpty();
            RuleForEach(x => x.Ids).GreaterThan(0);
        }
    }
}
