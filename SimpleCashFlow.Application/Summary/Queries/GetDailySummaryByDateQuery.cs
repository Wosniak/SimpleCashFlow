using SimpleCashFlow.Application.Abstractions.Queries;
using SimpleCashFlow.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCashFlow.Application.Summary.Queries
{
    public record GetDailySummaryByDateQuery(DateOnly date): IQuery<CashFlowDailySummary>;

}
