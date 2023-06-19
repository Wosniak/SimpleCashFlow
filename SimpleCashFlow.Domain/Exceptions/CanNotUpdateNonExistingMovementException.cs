namespace SimpleCashFlow.Domain.Exceptions
{
    public class CanNotUpdateNonExistingMovementException : Exception
    {
        public CanNotUpdateNonExistingMovementException(string message) : base(message)
        {

        }
    }
}
