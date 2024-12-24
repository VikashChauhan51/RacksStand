using Microsoft.EntityFrameworkCore;
using RacksStand.Abstraction.Repositories;
using RacksStand.Domain.Entities;

namespace RacksStand.Infrastructure.Persistence.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }
    public async virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity);
    }

    public async virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public async virtual Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull
    {
        return await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }
}
