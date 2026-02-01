using Application.ApplicationResult;
using Application.InfraInterfaces.Persistance;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, Result>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IValidator<UpdateArtistCommand> _validator;

        public UpdateArtistCommandHandler(
            IArtistRepository artistRepository,
            IValidator<UpdateArtistCommand> validator)
        {
            _artistRepository = artistRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateArtistCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var artist = await _artistRepository.GetById(command.Id);

            if (artist == null)
            {
                return Result.Failure(
                    new Error(
                        "ArtistNotFound",
                        $"Artist with ID {command.Id} was not found."
                    )
                );
            }

            artist.UpdateName(command.Name);

            await _artistRepository.CommitAsync();

            return Result.Success();
        }
    }
}
