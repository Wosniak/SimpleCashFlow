using SimpleCashFlow.Domain.DomainEvents.Base;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Domain.DomainEvents
{
    public record MovementCreatedDomainEvent(Guid Id, MovementId MovementId) : DomainEvent(Id);
}
