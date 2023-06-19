using SimpleCashFlow.Domain.ValueObjects;
using System.Threading;

namespace SimpleCashFlow.Domain.Abstractions.Repositories
{
    public interface ICashFlowDailySummaryRepository
    {
        Task AddAsync(CashFlowDailySummary dailySummary,CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<CashFlowDailySummary?> GetByDateAsync(DateOnly date, CancellationToken cancellationToken);

        Task<CashFlowDailySummary?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task RemoveCurrentSummaryForDateAsync(DateOnly date, CancellationToken cancellationToken);


    }
}
