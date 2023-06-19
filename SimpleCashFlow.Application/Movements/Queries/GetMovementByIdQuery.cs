using SimpleCashFlow.Application.Abstractions.Queries;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Application.Movements.Queries
{
    public record GetMovementByIdQuery(Guid Id) : IQuery<Movement>;

}
