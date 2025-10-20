namespace RacksStand.Api.Domain.Interceptors;

using System.Collections.Concurrent;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

public sealed class QueryTimingInterceptor : DbCommandInterceptor
{
    private readonly ILogger<QueryTimingInterceptor> logger;
    private readonly int warningThresholdMs;
    private readonly ConcurrentDictionary<Guid, Stopwatch> timers = new();

    public QueryTimingInterceptor(ILogger<QueryTimingInterceptor> logger, int warningThresholdMs = 500)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.warningThresholdMs = warningThresholdMs;
    }

    // ====== helpers ======
    private void Start(CommandEventData eventData)
        => timers[eventData.CommandId] = Stopwatch.StartNew();

    private long Stop(Guid commandId)
    {
        if (timers.TryRemove(commandId, out var sw))
        {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        return -1; // wasn't started or already removed
    }

    private void Log(DbCommand command, CommandEndEventData eventData, string kind, Exception? ex = null)
    {
        var elapsed = Stop(eventData.CommandId);

        // Choose level by duration / error
        if (ex != null)
        {
            logger.LogError(ex,
                "EF SQL {Kind} failed after {Elapsed} ms. ConnId={ConnId}. Command:\n{Sql}",
                kind, elapsed, eventData.ConnectionId, command.CommandText);
            return;
        }

        if (elapsed >= warningThresholdMs)
        {
            logger.LogWarning(
                "EF SQL {Kind} took {Elapsed} ms (slow). ConnId={ConnId}. Command:\n{Sql}",
                kind, elapsed, eventData.ConnectionId, command.CommandText);
        }
        else
        {
            logger.LogInformation(
                "EF SQL {Kind} took {Elapsed} ms. ConnId={ConnId}. Command:\n{Sql}",
                kind, elapsed, eventData.ConnectionId, command.CommandText);
        }
    }

    // ====== READER (queries returning rows) ======
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        Start(eventData);
        return base.ReaderExecuting(command, eventData, result);
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        Start(eventData);
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override DbDataReader ReaderExecuted(
        DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        Log(command, eventData, "Reader");
        return base.ReaderExecuted(command, eventData, result);
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(
        DbCommand command, CommandExecutedEventData eventData,
        DbDataReader result, CancellationToken cancellationToken = default)
    {
        Log(command, eventData, "Reader");
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    // ====== SCALAR (single value: COUNT, SUM, etc.) ======
    public override InterceptionResult<object> ScalarExecuting(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<object> result)
    {
        Start(eventData);
        return base.ScalarExecuting(command, eventData, result);
    }

    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<object> result, CancellationToken cancellationToken = default)
    {
        Start(eventData);
        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override object? ScalarExecuted(
        DbCommand command, CommandExecutedEventData eventData, object? result)
    {
        Log(command, eventData, "Scalar");
        return base.ScalarExecuted(command, eventData, result);
    }

    public override ValueTask<object?> ScalarExecutedAsync(
        DbCommand command, CommandExecutedEventData eventData,
        object? result, CancellationToken cancellationToken = default)
    {
        Log(command, eventData, "Scalar");
        return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
    }

    // ====== NON-QUERY (INSERT/UPDATE/DELETE, DDL) ======
    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<int> result)
    {
        Start(eventData);
        return base.NonQueryExecuting(command, eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        Start(eventData);
        return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override int NonQueryExecuted(
        DbCommand command, CommandExecutedEventData eventData, int result)
    {
        Log(command, eventData, "NonQuery");
        return base.NonQueryExecuted(command, eventData, result);
    }

    public override ValueTask<int> NonQueryExecutedAsync(
        DbCommand command, CommandExecutedEventData eventData,
        int result, CancellationToken cancellationToken = default)
    {
        Log(command, eventData, "NonQuery");
        return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
    }

    // ====== failure / cancel ======
    public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
    {
        Log(command, eventData, "Failed", eventData.Exception);
        base.CommandFailed(command, eventData);
    }

    public override Task CommandFailedAsync(
        DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        Log(command, eventData, "Failed", eventData.Exception);
        return base.CommandFailedAsync(command, eventData, cancellationToken);
    }

    public override void CommandCanceled(DbCommand command, CommandEndEventData eventData)
    {
        Log(command, eventData, "Canceled");
        base.CommandCanceled(command, eventData);
    }

    public override Task CommandCanceledAsync(
        DbCommand command, CommandEndEventData eventData, CancellationToken cancellationToken = default)
    {
        Log(command, eventData, "Canceled");
        return base.CommandCanceledAsync(command, eventData, cancellationToken);
    }
}
