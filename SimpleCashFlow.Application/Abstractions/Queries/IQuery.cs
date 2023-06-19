using MediatR;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Abstractions.Queries
{
    public interface IQuery<TResult> : IRequest<Result<TResult>>
    {
    }
}
