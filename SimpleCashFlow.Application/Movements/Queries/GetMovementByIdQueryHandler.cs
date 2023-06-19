using SimpleCashFlow.Application.Abstractions.Queries;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Movements.Queries
{
    internal class GetMovementByIdQueryHandler : IQueryHandler<GetMovementByIdQuery, Movement>
    {

        private readonly IMovementRepository _movementRepository;

        public GetMovementByIdQueryHandler(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        public async Task<Result<Movement>> Handle(GetMovementByIdQuery request, CancellationToken cancellationToken)
        {
            var movement = await _movementRepository.GetMovementByIdAsync(new MovementId(request.Id), cancellationToken);

            return movement;

        }
    }
}
