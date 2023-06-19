using SimpleCashFlow.Domain.DomainEvents.Base;

namespace SimpleCashFlow.Domain.Entities.Base
{
    public class Entity
    {
        private List<DomainEvent> _domainEvents = new();

        public IList<DomainEvent> DomainEvent => _domainEvents;

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public DateTime CreatedAt { get; protected set; }

        public DateTime ChangedAt { get; internal set; }

    }
}
