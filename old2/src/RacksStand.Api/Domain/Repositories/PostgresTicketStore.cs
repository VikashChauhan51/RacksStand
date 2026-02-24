namespace RacksStand.Api.Domain.Repositories;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Domain.Entities;

public sealed class PostgresTicketStore : ITicketStore
{
    private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

    public PostgresTicketStore(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }

    public async Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString("N");
        await RenewAsync(key, ticket);
        return key;
    }

    public async Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        var data = SerializeTicket(ticket);

        var expiresAt = ticket.Properties.ExpiresUtc;

        await using var ctx = contextFactory.CreateDbContext();

        var entity = await ctx.AuthTickets.FindAsync(key);
        if (entity == null)
        {
            entity = new CacheStore
            {
                Key = key,
                Value = data,
                ExpiresAt = expiresAt
            };
            ctx.AuthTickets.Add(entity);
        }
        else
        {
            entity.Value = data;
            entity.ExpiresAt = expiresAt;
        }

        await ctx.SaveChangesAsync();
    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        await using var ctx = contextFactory.CreateDbContext();

        var entity = await ctx.AuthTickets
            .Where(x => x.Key == key && (x.ExpiresAt == null || x.ExpiresAt > DateTimeOffset.UtcNow))
            .FirstOrDefaultAsync();

        if (entity == null)
            return null;

        return DeserializeTicket(entity.Value);
    }

    public async Task RemoveAsync(string key)
    {
        await using var ctx = contextFactory.CreateDbContext();
        var entity = await ctx.AuthTickets.FindAsync(key);
        if (entity != null)
        {
            ctx.AuthTickets.Remove(entity);
            await ctx.SaveChangesAsync();
        }
    }

    private static byte[] SerializeTicket(AuthenticationTicket ticket)
    {
        return TicketSerializer.Default.Serialize(ticket);
    }

    private static AuthenticationTicket? DeserializeTicket(byte[] data)
    {
        return TicketSerializer.Default.Deserialize(data);
    }
}
