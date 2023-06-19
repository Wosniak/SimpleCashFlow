using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Domain.Abstractions.Services
{
    public interface ICalculateCashFlowDailySummary
    {
        Task<CashFlowDailySummary> Calculate(DateOnly date);

    }
}
