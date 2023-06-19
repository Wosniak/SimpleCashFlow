namespace SimpleCashFlow.Domain.Exceptions
{
    public class CanNotDeleteNonExistingMovementException : Exception
    {
        public CanNotDeleteNonExistingMovementException(string message) : base(message)
        {

        }
    }
}
