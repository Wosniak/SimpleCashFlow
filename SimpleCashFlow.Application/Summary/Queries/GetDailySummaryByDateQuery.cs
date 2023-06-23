using SimpleCashFlow.Application.Abstractions.Queries;
using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Application.Summary.Queries
{
    public record GetDailySummaryByDateQuery(DateOnly date) : IQuery<CashFlowDailySummary>;

}
