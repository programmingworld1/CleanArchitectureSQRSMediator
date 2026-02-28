using Application.ApplicationResult;
using Application.InfraInterfaces.Persistance;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class DeleteArtistsCommandHandler : IRequestHandler<DeleteArtistsCommand, Result>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IValidator<DeleteArtistsCommand> _validator;

        public DeleteArtistsCommandHandler(
            IArtistRepository artistRepository,
            IValidator<DeleteArtistsCommand> validator)
        {
            _artistRepository = artistRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteArtistsCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var foundArtists = (await _artistRepository.GetAll(a => command.Ids.Contains(a.Id))).ToList();

            if (foundArtists.Count == 0)
            {
                return Result.Success();
            }

            _artistRepository.DeleteRange(foundArtists);

            await _artistRepository.CommitAsync();

            return Result.Success();
        }
    }
}
