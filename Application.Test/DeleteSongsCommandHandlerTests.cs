using Application.InfraInterfaces.Persistance;
using Application.Mediator.Song.Commands;
using Application.Mediator.Song.Validators;
using Domain.Entities;
using Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Application.Test
{
    public class DeleteSongsCommandHandlerTests
    {
        private readonly Mock<IArtistRepository> _artistRepositoryMock = new();
        private readonly Mock<IValidator<DeleteSongsCommand>> _validatorMock = new();

        private DeleteSongsCommandHandler CreateHandler() =>
            new(_artistRepositoryMock.Object, _validatorMock.Object);

        private void SetupValidatorSuccess()
        {
            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<DeleteSongsCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }

        private static Artist CreateArtistWithSong(string songName)
        {
            var artist = new Artist("Test Artist");
            artist.AddSong(new Song(songName, new Year(2020), new Bpm(140), new Duration(180)));
            return artist;
        }

        [Fact]
        public async Task Handle_AllIdsExist_RemovesSongsViaAggregateAndReturnsSuccess()
        {
            // Arrange
            var ids = new List<int> { 1, 2 };
            var command = new DeleteSongsCommand(ids);

            // Two artists, each owning one of the requested song IDs
            var artistA = CreateArtistWithSong("Song A");
            var artistB = CreateArtistWithSong("Song B");
            var artists = new List<Artist> { artistA, artistB };

            SetupValidatorSuccess();

            _artistRepositoryMock
                .Setup(r => r.GetArtistsBySongIds(ids))
                .ReturnsAsync(artists);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _artistRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_SomeIdsMissing_DeletesOnlyFoundSongsAndReturnsSuccess()
        {
            // Arrange — ID 99 is not owned by any returned artist → skipped
            var ids = new List<int> { 1, 99 };
            var command = new DeleteSongsCommand(ids);

            var artistA = CreateArtistWithSong("Song A");
            var artists = new List<Artist> { artistA };

            SetupValidatorSuccess();

            _artistRepositoryMock
                .Setup(r => r.GetArtistsBySongIds(ids))
                .ReturnsAsync(artists);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _artistRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyIds_ThrowsValidationException()
        {
            // Arrange — use the real validator so ValidateAndThrowAsync actually fires
            var command = new DeleteSongsCommand(new List<int>());
            var handler = new DeleteSongsCommandHandler(_artistRepositoryMock.Object, new DeleteSongsCommandValidator());

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(command, CancellationToken.None));

            _artistRepositoryMock.Verify(r => r.CommitAsync(), Times.Never);
        }
    }
}
