namespace RacksStand.Abstraction.Repositories;
public interface ITransaction
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    ValueTask DisposeAsync();
}
