using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace RacksStand.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private EfTransaction? _transaction;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }


    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) return;
        _transaction = new EfTransaction(await _context.Database.BeginTransactionAsync(cancellationToken));
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) throw new InvalidOperationException("No transaction has been started.");
        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    public ITransaction? GetCurrentTransaction() => _transaction;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private sealed class EfTransaction : ITransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;
        public EfTransaction(IDbContextTransaction dbContextTransaction) => _dbContextTransaction = dbContextTransaction;

        public async Task CommitAsync(CancellationToken cancellationToken = default) => await _dbContextTransaction.CommitAsync(cancellationToken);

        public async Task RollbackAsync(CancellationToken cancellationToken = default) => await _dbContextTransaction.RollbackAsync(cancellationToken);

        public ValueTask DisposeAsync() => _dbContextTransaction.DisposeAsync();
    }
}
