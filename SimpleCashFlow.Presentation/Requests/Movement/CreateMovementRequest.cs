namespace SimpleCashFlow.Presentation.Requests.Movement
{
    public record CreateMovementRequest(
        DateTime Date,
        decimal Amount,
        string Classificaion
    );


}
