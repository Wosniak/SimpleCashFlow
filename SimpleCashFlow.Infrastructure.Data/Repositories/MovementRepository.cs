using Microsoft.EntityFrameworkCore;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Infrastructure.Data.Context;

namespace SimpleCashFlow.Infrastructure.Data.Repositories
{
    public class MovementRepository : IMovementRepository
    {

        private readonly SimpleCashFlowContext _ctx;
        public MovementRepository(SimpleCashFlowContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Movement cashFlowMovement, CancellationToken cancellationToken)
        {
            await _ctx.Movements.AddAsync(cashFlowMovement, cancellationToken);
        }

        public Task Delete(Movement cashFlowMovement, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MovementExistsAsync(MovementId id, CancellationToken cancellationToken)
        {
            return await _ctx.Movements.AnyAsync(m => m.Id == id, cancellationToken);
        }

        public void Update(Movement cashFlowMovement)
        {
            _ctx.Movements.Update(cashFlowMovement);
        }

        public async Task<Movement?> GetMovementByIdAsync(MovementId id, CancellationToken cancellationToken)
        {
            var movement = await _ctx.FindAsync<Movement>(id, cancellationToken);
            return movement;
        }
    }
}
