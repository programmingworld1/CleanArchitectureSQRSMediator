using Application.ApplicationResult;
using Application.InfraInterfaces.Persistance;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, Result>
    {
        private readonly IArtistRepository _artistRepository;

        public DeleteSongCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<Result> Handle(DeleteSongCommand command, CancellationToken cancellationToken)
        {
            // Find artist that owns this song
            var artist = await _artistRepository.GetArtistBySongId(command.Id);

            if (artist == null)
            {
                return Result.Failure(
                    new Error(
                        "SongNotFound",
                        $"Song with ID {command.Id} was not found."
                    )
                );
            }

            artist.RemoveSong(command.Id);

            await _artistRepository.CommitAsync();

            return Result.Success();
        }
    }
}
