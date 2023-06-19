using static SimpleCashFlow.Domain.ValueObjects.CashFlowDailySummary;

namespace SimpleCashFlow.Domain.ValueObjects
{
    public record CashFlowDailySummary(Guid Id, DateOnly Date, decimal TotalCreditAmount, decimal TotalDebitAmount, decimal TotalAmount, List<MovementItem> MovementItems)
    {
        public record MovementItem(
                Guid Id,
                DateTime Date,
                decimal Amount,
                string Classification
            );
    }
}
