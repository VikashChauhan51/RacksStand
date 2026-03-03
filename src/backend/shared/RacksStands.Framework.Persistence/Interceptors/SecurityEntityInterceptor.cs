using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RacksStands.Framework.Persistence.Abstractions;

namespace RacksStands.Framework.Persistence.Interceptors;

public class SecurityEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

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
