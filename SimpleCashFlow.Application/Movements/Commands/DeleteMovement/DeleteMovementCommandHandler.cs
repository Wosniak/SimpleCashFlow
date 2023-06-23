using FluentValidation;
using SimpleCashFlow.Application.Abstractions.Commands;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Movements.Commands.DeleteMovement
{
    internal sealed class DeleteMovementCommandHandler : ICommandHandler<DeleteMovementCommand>
    {

        private readonly IMovementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeleteMovementCommand> _validator;

        public DeleteMovementCommandHandler(IMovementRepository repository, IUnitOfWork unitOfWork, IValidator<DeleteMovementCommand> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteMovementCommand request, CancellationToken cancellationToken)
        {

            var currentMovement = await _repository.GetMovementByIdAsync(new MovementId(request.Id), cancellationToken);

            if (currentMovement is null)
            {
                return Result.Fail(new Error("SimpleCashFlow.Movement.NotFound", $"Movement with id {request.Id} does not exists in database."));
            }

            currentMovement.Delete();

            _repository.Delete(currentMovement);

            _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

    }
}
