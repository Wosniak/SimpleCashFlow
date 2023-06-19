using MediatR;

namespace SimpleCashFlow.Domain.DomainEvents.Base
{
    public record DomainEvent(Guid Id) : INotification;
}
