using Application.Mediator.Artist.Commands;
using FluentValidation;

namespace Application.Mediator.Artist.Validators
{
    public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
    {
        public UpdateArtistCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.RowVersion)
                .NotNull()
                .NotEmpty()
                .WithMessage("RowVersion is required for updates.");
        }
    }
}
