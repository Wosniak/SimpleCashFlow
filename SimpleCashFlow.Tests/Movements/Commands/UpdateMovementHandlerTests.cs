using Moq;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Application.Movements.Commands.UpdateMovement;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Tests.Movements.Commands
{
    public class UpdateMovementHandlerTests
    {
        private readonly Mock<IMovementRepository> _movementRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateMovementHandlerTests()
        {
            _movementRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_WhenMovementValueIsZero()
        {
            //Arrange

            var movement = Movement.Create(new DateTime(2023, 06, 10, 10, 10, 0), 10, "Software Consultin for Company XPTO");

            _movementRepositoryMock.Setup(
                r => r.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    It.IsAny<CancellationToken>()
                   )).ReturnsAsync(movement);


            var command = new UpdateMovementCommand(movement.Id.Value, new DateTime(2023, 06, 10, 10, 10, 0), 0, "Software Consultin for Company XPTO");

            var handler = new UpdateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new UpdateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.HasValidationError);
            Assert.NotNull(result.Error);
            Assert.NotEmpty(result.Error.Code);
            Assert.NotEmpty(result.Error.Message);
            Assert.NotNull(result.Messages);
            Assert.True(result.Messages.Any());
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_WhenClassificationIsEmpty()
        {
            //Arrange

            var movement = Movement.Create(new DateTime(2023, 06, 10, 10, 10, 0), 10, "Software Consultin for Company XPTO");

            _movementRepositoryMock.Setup(
                r => r.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    It.IsAny<CancellationToken>()
                   )).ReturnsAsync(movement);


            var command = new UpdateMovementCommand(movement.Id.Value, new DateTime(2023, 06, 10, 10, 10, 0), 0, "Software Consultin for Company XPTO");

            var handler = new UpdateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new UpdateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.HasValidationError);
            Assert.NotNull(result.Error);
            Assert.NotEmpty(result.Error.Code);
            Assert.NotEmpty(result.Error.Message);
            Assert.NotNull(result.Messages);
            Assert.True(result.Messages.Any());
        }



        [Fact]
        public async Task Handle_Shoud_Return_Fail_WhenMovementDoesNotExists()
        {

            //Arrange

            _movementRepositoryMock.Setup(
                r => r.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    It.IsAny<CancellationToken>()
                   )).ReturnsAsync((Movement?)null);


            var command = new UpdateMovementCommand(Guid.NewGuid(), new DateTime(2023, 06, 10, 10, 10, 0), 10, "Software Consultin for Company XPTO");

            var handler = new UpdateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new UpdateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assset

            Assert.False(result.IsSuccess);
            Assert.False(result.HasValidationError);
            Assert.NotNull(result.Error);
            Assert.NotEmpty(result.Error.Code);
            Assert.NotEmpty(result.Error.Message);
            Assert.Null(result.Messages);

        }

        [Fact]
        public async Task Handle_Should_Return_Success()
        {
            //Arrange

            var movement = Movement.Create(new DateTime(2023, 06, 10, 10, 10, 0), 10, "Software Consultin for Company XPTO");

            _movementRepositoryMock.Setup(
                r => r.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    It.IsAny<CancellationToken>()
                   )).ReturnsAsync(movement);


            var command = new UpdateMovementCommand(movement.Id.Value, new DateTime(2023, 06, 10, 10, 10, 0), 100, "Software Consultin for Company XPTO");

            var handler = new UpdateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new UpdateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSuccess);

        }
    }
}
