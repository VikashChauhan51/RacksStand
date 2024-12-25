using RacksStand.Domain.Entities;
using RacksStand.Models;
using System.Linq.Expressions;

namespace RacksStand.Abstraction.Services;
public interface IService<TEntity, TId> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<PaginatedResult<TEntity>> GetPaginatedAsync(
    int pageNumber,
    int pageSize,
    bool includeCount,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TId id, CancellationToken cancellationToken);
}

