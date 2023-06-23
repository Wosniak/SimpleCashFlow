using Moq;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Application.Movements.Commands.DeleteMovement;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Tests.Movements.Commands
{
    public class DeleteMovementHandlerTests
    {
        private readonly Mock<IMovementRepository> _movementRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteMovementHandlerTests()
        {
            _movementRepositoryMock = new();
            _unitOfWorkMock = new();
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


            var command = new DeleteMovementCommand(Guid.NewGuid());

            var handler = new DeleteMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new DeleteMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assset

            Assert.False(result.IsSuccess);

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


            var command = new DeleteMovementCommand(movement.Id.Value);

            var handler = new DeleteMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new DeleteMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(movement.Deleted);

        }
    }
}
