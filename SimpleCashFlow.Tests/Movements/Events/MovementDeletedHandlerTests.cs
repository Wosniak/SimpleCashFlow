using Moq;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Application.Movements.Commands.DeleteMovement;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Abstractions.Services;
using SimpleCashFlow.Domain.DomainEvents;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Tests.Movements.Events
{
    public class MovementDeletedHandlerTests
    {
        private readonly Mock<ICashFlowDailySummaryRepository> _summaryRepositoryMock;
        private readonly Mock<IMovementRepository> _movementRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICalculateCashFlowDailySummary> _calculateCashFlowServiceMock;

        public MovementDeletedHandlerTests()
        {
            _summaryRepositoryMock = new();
            _movementRepositoryMock = new();
            _unitOfWorkMock = new();
            _calculateCashFlowServiceMock = new();
        }

        [Fact]
        public async Task Handle_Shoul_Run()
        {
            // Arrange

            var notification = new MovementDeletedDomainEvent(Guid.NewGuid(), new MovementId(Guid.NewGuid()));

            var handler = new MovementDeletedEventHandler(_summaryRepositoryMock.Object, _unitOfWorkMock.Object, _calculateCashFlowServiceMock.Object, _movementRepositoryMock.Object);

            _summaryRepositoryMock.Setup(
                s => s.GetByDateAsync(
                    It.IsAny<DateOnly>(),
                    default
                    )
            ).ReturnsAsync(new CashFlowDailySummary(Guid.NewGuid(), new DateOnly(2023, 06, 15), 200, -100, 100, new List<CashFlowDailySummary.MovementItem>()));


            _movementRepositoryMock.Setup(
                mr => mr.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    default
                    )
                ).ReturnsAsync(Movement.Create(DateTime.Now, 100, "Test"));


            _calculateCashFlowServiceMock.Setup(
                cs => cs.Calculate(
                    It.IsAny<DateOnly>()
                    )
                ).ReturnsAsync(new CashFlowDailySummary(Guid.NewGuid(), new DateOnly(2023, 06, 15), 100, -20, 80, new List<CashFlowDailySummary.MovementItem>()));

            //Act

            await handler.Handle(notification, default);

            //Assert
            _summaryRepositoryMock.Verify(sr => sr.RemoveCurrentSummaryForDateAsync(It.IsAny<DateOnly>(), default), Times.Once);
            _movementRepositoryMock.Verify(mr => mr.GetMovementByIdAsync(It.IsAny<MovementId>(), default), Times.Once);
            _calculateCashFlowServiceMock.Verify(s => s.Calculate(It.IsAny<DateOnly>()), Times.Once);
            _summaryRepositoryMock.Verify(sr => sr.AddAsync(It.IsAny<CashFlowDailySummary>(), default), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(default), Times.Once);

        }
    }
}
