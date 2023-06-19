namespace SimpleCashFlow.Presentation.Requests.Movement
{
    public record UpdateMovementRequest(
        DateTime Date,
        decimal Amount,
        string Classificaion
    );
}
