using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Cqrs.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Cqrs;

public sealed class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken ct = default) where TQuery : Abstractions.IQuery<TResponse>
    {
        ArgumentNullException.ThrowIfNull(query);
        var handler = serviceProvider.GetRequiredService<IHandler<TQuery, TResponse>>();

        var behaviors = serviceProvider.GetServices<IPipelineBehavior<TQuery, TResponse>>().ToList();

        RequestHandlerDelegate<TResponse> pipeline = ct => handler.HandleAsync(query, ct);

        for (var i = behaviors.Count - 1; i >= 0; i--)
        {
            var behavior = behaviors[i];
            var next = pipeline;
            pipeline = ct => behavior.HandleAsync(query, next, ct);
        }

        return pipeline(ct);
    }

    public Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken ct = default) where TCommand : ICommand<TResponse>
    {
        ArgumentNullException.ThrowIfNull(command);
        var handler = serviceProvider.GetRequiredService<IHandler<TCommand, TResponse>>();

        var behaviors = serviceProvider.GetServices<IPipelineBehavior<TCommand, TResponse>>().ToList();

        RequestHandlerDelegate<TResponse> pipeline = ct => handler.HandleAsync(command, ct);

        for (var i = behaviors.Count - 1; i >= 0; i--)
        {
            var behavior = behaviors[i];
            var next = pipeline;
            return behavior.HandleAsync(command, next, ct);
        }
        return pipeline(ct);
    }

    public Task SendAsync<TCommand>(TCommand command, CancellationToken ct = default) where TCommand : Abstractions.ICommand
    {
        ArgumentNullException.ThrowIfNull(command);
        var handler = serviceProvider.GetRequiredService<IHandler<TCommand, Unit>>();

        var behaviors = serviceProvider.GetServices<IPipelineBehavior<TCommand, Unit>>().ToList();

        RequestHandlerDelegate<Unit> pipeline = ct => handler.HandleAsync(command, ct);
        for (var i = behaviors.Count - 1; i >= 0; i--)
        {
            var behavior = behaviors[i];
            var next = pipeline;
            return behavior.HandleAsync(command, next, ct);
        }
        return pipeline(ct);
    }
}
