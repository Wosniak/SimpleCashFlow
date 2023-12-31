﻿using SimpleCashFlow.Application.Abstractions.Queries;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Results;
using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Application.Summary.Queries
{
    internal class GetDailySummaryByDateQueryHandler : IQueryHandler<GetDailySummaryByDateQuery, CashFlowDailySummary>
    {
        private readonly ICashFlowDailySummaryRepository _dailySummaryRepository;

        public GetDailySummaryByDateQueryHandler(ICashFlowDailySummaryRepository dailySummaryRepository)
        {
            _dailySummaryRepository = dailySummaryRepository;
        }

        public async Task<Result<CashFlowDailySummary>> Handle(GetDailySummaryByDateQuery request, CancellationToken cancellationToken)
        {
            var dailySummary = await _dailySummaryRepository.GetByDateAsync(request.date, cancellationToken);

            return dailySummary;
        }
    }
}
