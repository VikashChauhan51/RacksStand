namespace RacksStand.Api.Domain.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RacksStand.Api.Domain.Entities.Core;

public sealed class AuditEntityInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<AuditEntityInterceptor> logger;

    public AuditEntityInterceptor(ILogger<AuditEntityInterceptor> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        foreach (var entry in context.ChangeTracker.Entries<IEntityAudit>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    logger.LogInformation("Adding entity of type {EntityType},", entry.Entity.GetType().Name);
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    logger.LogInformation("Modifying entity of type {EntityType},", entry.Entity.GetType().Name);
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Deleted:
                    logger.LogInformation("Deleting entity of type {EntityType},", entry.Entity.GetType().Name);
                    entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                    break;
            }
        }

        foreach (var entry in context.ChangeTracker.Entries<IEntityConcurrency>())
        {
            if (string.IsNullOrEmpty(entry.Entity.ConcurrencyStamp))
            {
                entry.Entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
