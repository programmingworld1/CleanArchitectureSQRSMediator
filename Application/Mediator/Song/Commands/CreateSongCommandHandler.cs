using Application.ApplicationResult;
using Application.Errors;
using Application.InfraInterfaces.Persistance;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, Result>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;
        private readonly IValidator<CreateSongCommand> _validator;

        public CreateSongCommandHandler(IMapper mapper,
            IArtistRepository artistRepository,
            IValidator<CreateSongCommand> validator)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateSongCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var artist = await _artistRepository.GetArtistIncludingSongs(command.ArtistName.Trim());

            if(artist == null)
            {
                return Result.Failure(
                    new Error(
                        "ArtistNotFound",
                        $"Artist with name {command.ArtistName} was not found."
                    )
                );
            }

            var song = _mapper.Map<Domain.Entities.Song>(command);

            artist.AddSong(song);

            await _artistRepository.CommitAsync();

            return Result.Success();
        }
    }
}
