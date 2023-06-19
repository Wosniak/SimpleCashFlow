using Microsoft.EntityFrameworkCore;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.ValueObjects;
using SimpleCashFlow.Infrastructure.Data.Context;

namespace SimpleCashFlow.Infrastructure.Data.Repositories
{
    public class CashFlowDailySummaryRepository : ICashFlowDailySummaryRepository

    {
        readonly SimpleCashFlowContext _ctx;

        public CashFlowDailySummaryRepository(SimpleCashFlowContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(CashFlowDailySummary dailySummary, CancellationToken cancellationToken)
        {
            await _ctx.AddAsync(dailySummary, cancellationToken);
        }


        public async Task<CashFlowDailySummary?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await _ctx.Summaries.FindAsync(id, cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var summary = await GetByIdAsync(id, cancellationToken);

            if (summary != null)
            {
                _ctx.Summaries.Remove(summary);
            }
        }

        public async Task<CashFlowDailySummary?> GetByDateAsync(DateOnly date, CancellationToken cancellationToken)
        {
            return await _ctx.Summaries.FirstOrDefaultAsync(s => s.Date == date, cancellationToken);
        }

        public async Task RemoveCurrentSummaryForDateAsync(DateOnly date, CancellationToken cancellationToken)
        {
            var summary = await GetByDateAsync(date, cancellationToken);
            if (summary != null)
            {
                _ctx.Summaries.Remove(summary);
            }
        }
    }
}
