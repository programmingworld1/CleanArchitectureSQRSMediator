using Application.ApplicationResult;
using Application.InfraInterfaces.Persistance;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, Result>
    {
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;

        public DeleteSongCommandHandler(
            ISongRepository songRepository,
            IArtistRepository artistRepository)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
        }

        public async Task<Result> Handle(DeleteSongCommand command, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetById(command.Id);

            if (song == null)
            {
                return Result.Failure(
                    new Error(
                        "SongNotFound",
                        $"Song with ID {command.Id} was not found."
                    )
                );
            }

            _songRepository.Delete(song);

            await _artistRepository.CommitAsync();

            return Result.Success();
        }
    }
}
