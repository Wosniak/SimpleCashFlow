using FluentValidation;


namespace SimpleCashFlow.Application.Movements.Commands.DeleteMovement
{
    public sealed class DeleteMovementCommandValidator : AbstractValidator<DeleteMovementCommand>
    {
        public DeleteMovementCommandValidator()
        {
            RuleFor(c => c.Id).NotNull().WithErrorCode("SimpleCashFlow.Id.Null").WithMessage("A Cash Flow must have an id for delete.");

        }
    }
}
