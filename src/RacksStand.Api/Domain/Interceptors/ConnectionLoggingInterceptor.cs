namespace RacksStand.Api.Domain.Interceptors;

using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

public class ConnectionLoggingInterceptor : DbConnectionInterceptor
{
    private readonly ILogger<ConnectionLoggingInterceptor> logger;

    public ConnectionLoggingInterceptor(ILogger<ConnectionLoggingInterceptor> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public override async Task ConnectionOpenedAsync(
        DbConnection connection,
        ConnectionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        logger.LogDebug("[Connection Opened] {DataSource}/{Database}", connection.DataSource, connection.Database);
        await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
    }

    public override async Task ConnectionClosedAsync(
        DbConnection connection,
        ConnectionEndEventData eventData)
    {
        logger.LogDebug("[Connection Closed] {DataSource}/{Database}", connection.DataSource, connection.Database);
        await base.ConnectionClosedAsync(connection, eventData);
    }
}

