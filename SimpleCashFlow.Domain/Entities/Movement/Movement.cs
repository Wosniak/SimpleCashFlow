using SimpleCashFlow.Domain.DomainEvents;
using SimpleCashFlow.Domain.Entities.Base;
using SimpleCashFlow.Domain.Exceptions;

namespace SimpleCashFlow.Domain.Entities
{
    public class Movement : Entity
    {

        private Movement(MovementId id, DateTime date, decimal amount, string classification)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Classification = classification;
        }

        private Movement()
        {
            Id = new MovementId(Guid.NewGuid());
        }


        public MovementId Id { get; private set; }

        public DateTime Date { get; private set; }

        public decimal Amount { get; private set; }

        public string MovementType { get => Amount < 0 ? "D" : "C"; }

        public string Classification { get; private set; } = string.Empty;

        public bool Deleted { get; private set; }

        public static Movement Create(DateTime date, decimal amount, string classification)
        {
            var movement = new Movement()
            {
                Date = date,
                Amount = amount,
                Classification = classification,
                CreatedAt = DateTime.Now,
            };

            movement.Raise(new MovementCreatedDomainEvent(Guid.NewGuid(), movement.Id));
            return movement;
        }

        public void Update(DateTime date, decimal amount, string classificaion)
        {
            if (Id is null)
            {
                throw new CanNotUpdateNonExistingMovementException("The movement been update does not exists.");
            }

            Date = date;
            Amount = amount;
            Classification = classificaion;
            ChangedAt = DateTime.Now;

            Raise(new MovementUpdatedDomainEvent(Guid.NewGuid(), Id));

        }

        public void Delete()
        {
            if (Id is null)
            {
                throw new CanNotDeleteNonExistingMovementException("The movement been deleted does not exists.");
            }

            Deleted = true;
            ChangedAt = DateTime.Now;

            Raise(new MovementDeletedDomainEvent(Guid.NewGuid(), Id));

        }
    }

}
