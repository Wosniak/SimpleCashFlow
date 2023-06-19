using Moq;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Application.Movements.Commands.CreateMovement;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Abstractions.Services;
using SimpleCashFlow.Domain.DomainEvents;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.ValueObjects;
using SimpleCashFlow.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCashFlow.Tests.Movements.Events
{
    public class MovementCreatedHandlerTests
    {
        private readonly Mock<ICashFlowDailySummaryRepository> _summaryRepository;
        private readonly Mock<IMovementRepository> _movementRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICalculateCashFlowDailySummary> _calculateCashFlowService;

        public MovementCreatedHandlerTests()
        {
            _summaryRepository = new();
            _movementRepository = new();
            _unitOfWork = new();
            _calculateCashFlowService = new();
        }

        [Fact]
        public async Task Handle_Shoul_Run()
        {
            // Arrange

            var notification = new MovementCreatedDomainEvent(Guid.NewGuid(), new MovementId(Guid.NewGuid()));

            var handler = new MovementCreatedEventHandler(_summaryRepository.Object, _unitOfWork.Object, _calculateCashFlowService.Object, _movementRepository.Object);

            _summaryRepository.Setup(
                s => s.GetByDateAsync(
                    It.IsAny<DateOnly>(),
                    default
                    )
            ).ReturnsAsync(new CashFlowDailySummary(Guid.NewGuid(), new DateOnly(2023,06,15), 200,-100,100, new List<CashFlowDailySummary.MovementItem>()));


            _movementRepository.Setup(
                mr => mr.GetMovementByIdAsync(
                    It.IsAny<MovementId>(),
                    default
                    )
                ).ReturnsAsync(Movement.Create(DateTime.Now, 100, "Test"));


            _calculateCashFlowService.Setup(
                cs => cs.Calculate(
                    It.IsAny<DateOnly>()
                    )
                ).ReturnsAsync(new CashFlowDailySummary(Guid.NewGuid(), new DateOnly(2023, 06, 15), 100, -20, 80, new List<CashFlowDailySummary.MovementItem>()));

            //Act

            await handler.Handle(notification, default);

            //Assert
            _summaryRepository.Verify(sr =>sr.RemoveCurrentSummaryForDateAsync(It.IsAny<DateOnly>(), default),Times.Once);
            _movementRepository.Verify(mr => mr.GetMovementByIdAsync(It.IsAny<MovementId>(), default), Times.Once);
            _calculateCashFlowService.Verify(s => s.Calculate(It.IsAny<DateOnly>()), Times.Once);
            _summaryRepository.Verify(sr => sr.AddAsync(It.IsAny<CashFlowDailySummary>(), default), Times.Once);
            _unitOfWork.Verify(u =>  u.SaveChangesAsync(default), Times.Once);

            



        }
    }
}
