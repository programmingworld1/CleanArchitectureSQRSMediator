using Application.Errors;
using Application.InfraInterfaces.Persistance;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand>
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

        public async Task Handle(CreateSongCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var artist = await _artistRepository.GetArtistIncludingSongs(command.ArtistName.Trim());

            if(artist == null)
            {
                throw new NotFoundException(nameof(Artist), command.ArtistName);
            }

            var song = _mapper.Map<Domain.Entities.Song>(command);

            artist.AddSong(song);

            await _artistRepository.CommitAsync();
        }
    }
}
