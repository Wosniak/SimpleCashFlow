using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Application.Movements.Commands.CreateMovement;
using SimpleCashFlow.Domain.Abstractions.Repositories;

namespace SimpleCashFlow.Tests.Movements.Commands
{
    public class CreateMovementHandlerTests
    {
        private readonly Mock<IMovementRepository> _movementRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateMovementHandlerTests()
        {
            _movementRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_WhenMovementValueIsZero()
        {
            //Arrange

            var command = new CreateMovementCommand(new DateTime(2023, 06, 10, 10, 10, 0), 0, "Software Consultin for Company XPTO");

            var handler = new CreateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new CreateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.HasValidationError);
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_WhenClassificationIsEmpty()
        {
            //Arrange

            var command = new CreateMovementCommand(new DateTime(2023, 06, 10, 10, 10, 0), 10.99m, string.Empty);

            var handler = new CreateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new CreateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.HasValidationError);
        }

        [Fact]
        public async Task Handle_Should_Return_Success()
        { 
            //Arrange

            var command = new CreateMovementCommand(new DateTime(2023, 06, 10, 10, 10, 0), 10.99m, "Software Consultin for Company XPTO");

            var handler = new CreateMovementCommandHandler(_movementRepositoryMock.Object, _unitOfWorkMock.Object, new CreateMovementCommandValidator());

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(10.99m, result.Value.Amount);
            Assert.Equal(new DateTime(2023, 06, 10, 10, 10, 0), result.Value.Date);
            Assert.Equal("Software Consultin for Company XPTO", result.Value.Classification);
            Assert.True(result.Value.DomainEvent.Any());


        }

    }
}