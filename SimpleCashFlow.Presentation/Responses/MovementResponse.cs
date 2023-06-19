namespace SimpleCashFlow.Presentation.Responses
{
    public record MovementResponse(
        Guid Id,
        DateTime Date,
        decimal Amount,
        string MovementType,
        string Classification);

}
