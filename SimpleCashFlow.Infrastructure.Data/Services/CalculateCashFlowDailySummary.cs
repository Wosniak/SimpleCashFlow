using Microsoft.EntityFrameworkCore;
using SimpleCashFlow.Domain.Abstractions.Services;
using SimpleCashFlow.Domain.ValueObjects;
using SimpleCashFlow.Infrastructure.Data.Context;

namespace SimpleCashFlow.Domain.Services
{
    public class CalculateCashFlowDailySummary : ICalculateCashFlowDailySummary
    {
        private SimpleCashFlowContext _ctx;

        public CalculateCashFlowDailySummary(SimpleCashFlowContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<CashFlowDailySummary> Calculate(DateOnly date)
        {
            var summaryId = Guid.NewGuid();

            var summary = _ctx.Movements
                                .Where(m => DateOnly.FromDateTime(m.Date) == date)
                                .GroupBy(m => date)
                                .Select(gm => new CashFlowDailySummary(
                                        summaryId,
                                        date,
                                        gm.Where(m => m.Amount > 0).Sum(m => m.Amount),
                                        gm.Where(m => m.Amount < 0).Sum(m => m.Amount),
                                        gm.Sum(m => m.Amount),
                                        gm.Select(m => new CashFlowDailySummary.MovementItem(m.Id.Value, m.Date, m.Amount, m.Classification)).ToList()
                                    ));


            CashFlowDailySummary? cashFlowDailySummary = await summary.FirstOrDefaultAsync()!;
            return cashFlowDailySummary!;



        }
    }
}
