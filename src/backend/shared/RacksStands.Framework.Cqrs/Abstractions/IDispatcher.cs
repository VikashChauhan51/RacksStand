using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Cqrs.Abstractions;

public interface IDispatcher
{

    Task<TResponse> SendAsync<TCommand, TResponse>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand<TResponse>;
     Task SendAsync<TCommand>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand;

    Task<TResponse> QueryAsync<TQuery, TResponse>(
        TQuery query,
        CancellationToken ct = default)
        where TQuery : IQuery<TResponse>;
}
