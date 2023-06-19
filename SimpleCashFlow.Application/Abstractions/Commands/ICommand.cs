using MediatR;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Abstractions.Commands
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResult> : IRequest<Result<TResult>>
    {
    }
}
