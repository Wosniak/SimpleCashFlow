using SimpleCashFlow.Application.Abstractions.Commands;

namespace SimpleCashFlow.Application.Movements.Commands.UpdateMovement
{
    public record UpdateMovementCommand(
        Guid Id,
        DateTime Date,
        decimal Amount,
        string Classification
    ) : ICommand;


}
