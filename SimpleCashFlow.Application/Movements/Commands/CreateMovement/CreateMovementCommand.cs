using SimpleCashFlow.Application.Abstractions.Commands;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Application.Movements.Commands.CreateMovement
{
    public record CreateMovementCommand(
        DateTime Date,
        decimal Amount,
        string Classificaion
    ) : ICommand<Movement>;


}
