using MediatR;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
       where TQuery : IQuery<TResult>
    {
    }
}
