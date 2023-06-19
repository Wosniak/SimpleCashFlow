using SimpleCashFlow.Domain.Entities;


namespace SimpleCashFlow.Domain.Abstractions.Repositories
{
    public interface IMovementRepository
    {
        Task AddAsync(Movement cashFlowMovement, CancellationToken cancellationToken);

        void Update(Movement cashFlowMovement);

        Task Delete(Movement cashFlowMovement, CancellationToken cancellationToken);

        Task<bool> MovementExistsAsync(MovementId id, CancellationToken cancellationToken);

        Task<Movement?> GetMovementByIdAsync(MovementId id, CancellationToken cancellationToken);
    }
}
