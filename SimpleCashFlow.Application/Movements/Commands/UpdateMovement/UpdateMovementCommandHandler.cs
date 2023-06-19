using FluentValidation;
using SimpleCashFlow.Application.Abstractions.Commands;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Movements.Commands.UpdateMovement
{
    internal sealed class UpdateMovementCommandHandler : ICommandHandler<UpdateMovementCommand>
    {

        private readonly IMovementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateMovementCommand> _validator;

        public UpdateMovementCommandHandler(IMovementRepository repository, IUnitOfWork unitOfWork, IValidator<UpdateMovementCommand> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateMovementCommand request, CancellationToken cancellationToken)
        {

            var result = await ValidateParameters(request, cancellationToken);

            if (!result.IsSuccess)
            {
                return result;
            }

            var currentMovement = await _repository.GetMovementByIdAsync(new MovementId(request.Id), cancellationToken);

            if (currentMovement is null)
            {
                return Result.Fail(new Error("SimpleCashFlow.Movement.NotFound", $"Movement with id {request.Id} does not exists in database."));
            }

            currentMovement.Update(request.Date, request.Amount, request.Classification);

            _repository.Update(currentMovement);

            _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        private async Task<Result> ValidateParameters(UpdateMovementCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(err => new Error(err.ErrorCode, err.ErrorMessage)).ToList();

                return Result.ValidationFailResult(errors);
            }

            return Result.Success();
        }
    }
}
