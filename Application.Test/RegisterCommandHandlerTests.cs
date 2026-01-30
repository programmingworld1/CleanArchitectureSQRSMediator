using Application.Errors;
using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using Application.Mediator.Authentication.Commands.Register;
using Domain.Entities;
using MapsterMapper;
using Moq;
using Xunit;

namespace Application.Test
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapsterMapperMock = new();
        private readonly Mock<IJwtTokenGenerator> _tokenGeneratorMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        private CreateUserCommandHandler CreateHandler()
        {
            return new CreateUserCommandHandler(
                _mapsterMapperMock.Object,
                _tokenGeneratorMock.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_RegisterCommand_AddUser()
        {
            // Arrange
            var command = new CreateUserCommand 
            { 
                Email = "test@teamrockstars.nl", 
                FirstName = "FirstName", 
                LastName = "LastName", 
                Password = "123"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmail(command.Email))
                .Returns((User?)null);

            _mapsterMapperMock
                .Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>()))
                .Returns((CreateUserCommand c) => new User
                {
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Password = c.Password
                });

            var handler = CreateHandler();

            // Act + Assert
            var result = await handler.Handle(command, CancellationToken.None);

            _userRepositoryMock.Verify(
                repo => repo.Add(It.Is<User>(u =>
                    u.Email == command.Email &&
                    u.FirstName == command.FirstName &&
                    u.LastName == command.LastName &&
                    u.Password == command.Password
                )),
                Times.Once
            );

            Assert.Equal(command.Email, result.User.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("test@live.nl")]
        [InlineData("test@rockstar.nl")]
        public async Task Handle_WrongEmail_ThrowsWrongEmailException(string email)
        {
            // Arrange
            var command = new CreateUserCommand { Email = email };

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmail(command.Email))
                .Returns((User?)null);

            var handler = CreateHandler();

            // Act + Assert
            await Assert.ThrowsAsync<WrongEmailException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserAlreadyExits_ThrowsDuplicateEmailException()
        {
            // Arrange
            var command = new CreateUserCommand { Email = "test@teamrockstars.nl" };

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmail(command.Email))
                .Returns(new Domain.Entities.User());

            var handler = CreateHandler();

            // Act + Assert
            await Assert.ThrowsAsync<DuplicateEmailException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
