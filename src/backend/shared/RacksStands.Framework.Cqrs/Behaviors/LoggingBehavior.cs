using Microsoft.Extensions.Logging;
using RacksStands.Framework.Cqrs.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Cqrs.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct = default)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);

        var stopWatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            var response  = await next(ct).ConfigureAwait(false);
            stopWatch.Stop();
            logger.LogInformation(
                "Handled {RequestName} in {ElapsedMs} ms",
                requestName,
                stopWatch.ElapsedMilliseconds);

            return response;

        }
        catch (System.Exception ex)
        {
            stopWatch.Stop();
            logger.LogError(
                ex,
                "Error handling {RequestName} after {ElapsedMs} ms",
                requestName,
                stopWatch.ElapsedMilliseconds);

            throw;
        }
    }
}
