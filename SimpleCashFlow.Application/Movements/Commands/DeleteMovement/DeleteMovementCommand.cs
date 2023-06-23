using SimpleCashFlow.Application.Abstractions.Commands;

namespace SimpleCashFlow.Application.Movements.Commands.DeleteMovement
{
    public record DeleteMovementCommand(
        Guid Id
    ) : ICommand;


}
