namespace RacksStand.Abstraction.Repositories;
public interface IUnitOfWork<TContext> where TContext : class
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    ITransaction? GetCurrentTransaction();
}
