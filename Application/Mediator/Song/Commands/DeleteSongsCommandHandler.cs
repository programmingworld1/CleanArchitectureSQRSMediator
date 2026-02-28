using Application.ApplicationResult;
using Application.InfraInterfaces.Persistance;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class DeleteSongsCommandHandler : IRequestHandler<DeleteSongsCommand, Result>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IValidator<DeleteSongsCommand> _validator;

        public DeleteSongsCommandHandler(
            IArtistRepository artistRepository,
            IValidator<DeleteSongsCommand> validator)
        {
            _artistRepository = artistRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteSongsCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var artists = await _artistRepository.GetArtistsBySongIds(command.Ids);

            if (artists.Count == 0)
            {
                return Result.Success();
            }

            foreach (var id in command.Ids)
            {
                var owner = artists.FirstOrDefault(a => a.Songs.Any(s => s.Id == id));

                if (owner is null) continue;

                owner.RemoveSong(id);
            }

            await _artistRepository.CommitAsync();

            return Result.Success();
        }
    }
}
