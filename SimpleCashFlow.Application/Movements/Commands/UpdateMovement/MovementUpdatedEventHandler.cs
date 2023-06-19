using MediatR;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Abstractions.Services;
using SimpleCashFlow.Domain.DomainEvents;

namespace SimpleCashFlow.Application.Movements.Commands.UpdateMovement
{
    internal sealed class MovementUpdatedEventHandler : INotificationHandler<MovementUpdatedDomainEvent>
    {
        private readonly ICashFlowDailySummaryRepository _summaryRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalculateCashFlowDailySummary _calculateCashFlowService;

        public MovementUpdatedEventHandler(ICashFlowDailySummaryRepository repository, IUnitOfWork unitOfWork, ICalculateCashFlowDailySummary calculateCashFlowService, IMovementRepository movementRepository)
        {
            _summaryRepository = repository;
            _unitOfWork = unitOfWork;
            _calculateCashFlowService = calculateCashFlowService;
            _movementRepository = movementRepository;
        }

        public async Task Handle(MovementUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var movement = await _movementRepository.GetMovementByIdAsync(notification.MovementId, cancellationToken);

            var summary = await _calculateCashFlowService.Calculate(DateOnly.FromDateTime(movement!.Date));

            if (summary != null)
            {
                await _summaryRepository.RemoveCurrentSummaryForDateAsync(DateOnly.FromDateTime(movement.Date), cancellationToken);

                await _summaryRepository.AddAsync(summary, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }


    }
}
