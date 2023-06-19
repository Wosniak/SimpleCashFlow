using MediatR;
using SimpleCashFlow.Domain.Results;

namespace SimpleCashFlow.Application.Abstractions.Commands
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
       where TCommand : ICommand<TResult>
    {
    }
}
