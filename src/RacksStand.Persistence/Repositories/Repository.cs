using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Models;
using System.Linq.Expressions;

namespace RacksStand.Persistence.Repositories;
public abstract class Repository<TEntity, TId>
    : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    protected readonly DbContext Context;

    protected Repository(DbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual void Add(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Context.Set<TEntity>().Add(entity);
    }

    public async virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
    }

    public virtual void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        if (Context.Entry(entity).State == EntityState.Detached)
        {
            Context.Set<TEntity>().Attach(entity);
        }
        Context.Set<TEntity>().Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        Context.Set<TEntity>().RemoveRange(entities);
    }

    public virtual IQueryable<TEntity> GetAll(bool trackChanges = false)
    {
        var query = Context.Set<TEntity>().AsQueryable();
        return trackChanges ? query : query.AsNoTracking();
    }

    public async virtual Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await Context.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken).ConfigureAwait(false);
        return entity;
    }

    public async Task<PaginatedResult<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize, bool includeCount, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = this.GetAll();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        int totalItems = 0;
        if (includeCount)
        {
            totalItems = await query.CountAsync(cancellationToken).ConfigureAwait(false);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PaginatedResult<TEntity>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public virtual void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        Context.Set<TEntity>().UpdateRange(entities);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
