using Moq;
using SimpleCashFlow.Application.Movements.Queries;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Tests.Movements.Queries
{
    public class GetMovementByIdQueryTests
    {
        private readonly Mock<IMovementRepository> _movementRepositoryMock;

        public GetMovementByIdQueryTests()
        {
            _movementRepositoryMock = new();
        }


        [Fact]
        public async Task Query_Shoud_ReturnResultWithNullValue()
        {

            //Arrange
            Movement? movement = null;

            _movementRepositoryMock.Setup(
                mr => mr.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    default
                    )
                ).ReturnsAsync(movement!);

            var query = new GetMovementByIdQuery(new MovementId(Guid.NewGuid()));

            var handler = new GetMovementByIdQueryHandler(_movementRepositoryMock.Object);

            //Act

            var result = await handler.Handle(query, default);

            //Assert

            Assert.True(result.IsSuccess);
            Assert.Null(result.Value);
        }


        [Fact]
        public async Task Query_Shoud_ReturnResultWithValue()
        {

            //Arrange
            var movement = Movement.Create(new DateTime(2023, 06, 10, 10, 10, 0), 10, "Software Consultin for Company XPTO");

            _movementRepositoryMock.Setup(
                r => r.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    It.IsAny<CancellationToken>()
                   )).ReturnsAsync(movement);

            var query = new GetMovementByIdQuery(movement.Id);

            var handler = new GetMovementByIdQueryHandler(_movementRepositoryMock.Object);

            //Act

            var result = await handler.Handle(query, default);

            //Assert

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(movement.Id, result.Value.Id);
        }
    }
}
