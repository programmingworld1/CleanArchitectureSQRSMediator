using Application.InfraInterfaces.Persistance;
using Application.Mediator.Artist.Commands;
using Application.Mediator.Artist.Validators;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Application.Test
{
    public class DeleteArtistsCommandHandlerTests
    {
        private readonly Mock<IArtistRepository> _artistRepositoryMock = new();
        private readonly Mock<IValidator<DeleteArtistsCommand>> _validatorMock = new();

        private DeleteArtistsCommandHandler CreateHandler() =>
            new(_artistRepositoryMock.Object, _validatorMock.Object);

        private void SetupValidatorSuccess()
        {
            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<DeleteArtistsCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }

        [Fact]
        public async Task Handle_AllIdsExist_DeletesAllAndReturnsSuccess()
        {
            // Arrange
            var ids = new List<int> { 1, 2 };
            var command = new DeleteArtistsCommand(ids);
            var artists = new List<Artist> { new("Artist A"), new("Artist B") };

            SetupValidatorSuccess();

            _artistRepositoryMock
                .Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<Artist, bool>>>()))
                .ReturnsAsync(artists);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _artistRepositoryMock.Verify(r => r.DeleteRange(artists), Times.Once);
            _artistRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_SomeIdsMissing_DeletesOnlyFoundArtistsAndReturnsSuccess()
        {
            // Arrange
            var ids = new List<int> { 1, 2, 99 };
            var command = new DeleteArtistsCommand(ids);
            var foundArtists = new List<Artist> { new("Artist A"), new("Artist B") };

            SetupValidatorSuccess();

            _artistRepositoryMock
                .Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<Artist, bool>>>()))
                .ReturnsAsync(foundArtists);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _artistRepositoryMock.Verify(r => r.DeleteRange(foundArtists), Times.Once);
            _artistRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyIds_ThrowsValidationException()
        {
            // Arrange — use the real validator so ValidateAndThrowAsync actually fires
            var command = new DeleteArtistsCommand(new List<int>());
            var handler = new DeleteArtistsCommandHandler(_artistRepositoryMock.Object, new DeleteArtistsCommandValidator());

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(command, CancellationToken.None));

            _artistRepositoryMock.Verify(r => r.DeleteRange(It.IsAny<IEnumerable<Artist>>()), Times.Never);
            _artistRepositoryMock.Verify(r => r.CommitAsync(), Times.Never);
        }
    }
}
