using Application.Mediator.LibraryImporter.Models;
using FluentValidation;

namespace Application.Mediator.LibraryImporter.Validators
{
    public class SongImportDtoValidator : AbstractValidator<SongJsonDto>
    {
        public SongImportDtoValidator()
        {
            // Essentieel om te kunnen importeren
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(x => x.Artist)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Year)
                .InclusiveBetween(1800, 2100)
                .When(x => x.Year != 0);

            RuleFor(x => x.Duration)
                .GreaterThan(0)
                .When(x => x.Duration != 0);

            RuleFor(x => x.Bpm)
                .NotNull()
                .GreaterThan(0)
                .When(x => x.Bpm != null);

            RuleFor(x => x.Genre)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.SpotifyId)
                .MaximumLength(50);

            RuleFor(x => x.Album)
                .MaximumLength(200);

            RuleFor(x => x.Shortname)
                .MaximumLength(200);
        }
    }
}
