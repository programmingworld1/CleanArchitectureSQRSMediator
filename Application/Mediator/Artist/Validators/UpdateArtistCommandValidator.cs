using Application.Mediator.Artist.Commands;
using FluentValidation;

namespace Application.Mediator.Artist.Validators
{
    public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
    {
        public UpdateArtistCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            // Add more validation rules as needed
        }
    }
}
