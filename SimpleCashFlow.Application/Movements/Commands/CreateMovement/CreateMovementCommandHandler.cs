using FluentValidation;
using SimpleCashFlow.Application.Abstractions.Commands;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Movements.Commands.CreateMovement
{
    internal sealed class CreateMovementCommandHandler : ICommandHandler<CreateMovementCommand, Movement>
    {

        private readonly IMovementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateMovementCommand> _validator;

        public CreateMovementCommandHandler(IMovementRepository repository, IUnitOfWork unitOfWork, IValidator<CreateMovementCommand> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Movement>> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
        {

            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(err => new Error(err.ErrorCode, err.ErrorMessage)).ToList();

                return Result.ValidationFailResult<Movement>(errors);
            }

            var movement = Movement.Create(
                request.Date,
                request.Amount,
                request.Classificaion
                );

            await _repository.AddAsync(movement, cancellationToken);

            _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (Result<Movement>)movement;
        }
    }
}
