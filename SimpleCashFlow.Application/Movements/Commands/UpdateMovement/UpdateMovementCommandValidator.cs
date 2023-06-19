using FluentValidation;

namespace SimpleCashFlow.Application.Movements.Commands.UpdateMovement
{
    public sealed class UpdateMovementCommandValidator : AbstractValidator<UpdateMovementCommand>
    {
        public UpdateMovementCommandValidator()
        {
            RuleFor(c => c.Id).NotNull().WithErrorCode("SimpleCashFlow.Id.Null").WithMessage("A Cash Flow must have an id for update.");
            RuleFor(c => c.Amount).NotEqual(0).WithErrorCode("SimpleCashFlow.Amount.Zero").WithMessage("A Cash Flow item must have a valid amount, different from zero.");
            RuleFor(c => c.Date).NotNull().WithErrorCode("SimpleCashFlow.Date.IsNull").WithMessage("Date could not be null.");
            RuleFor(c => c.Classification).NotEmpty().WithErrorCode("SimpleCashFlow.Classification.Empty").WithMessage("A cashflow classification must be provided")
                                         .MaximumLength(150).WithErrorCode("SimpleCashFlow.Classification.TooLong").WithMessage("Cashflow classification is too long. The maximum lenght is 150 chars");
        }
    }
}
