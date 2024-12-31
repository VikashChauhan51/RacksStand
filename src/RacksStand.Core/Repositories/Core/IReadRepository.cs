using RacksStand.Domain.Core;
using RacksStand.Models;
using System.Linq.Expressions;

namespace RacksStand.Core.Repositories.Core;
public interface IReadRepository<TEntity, TId> : IDisposable where TEntity : IEntity<TId> where TId : notnull
{
    IQueryable<TEntity> GetAll(bool trackChanges = false);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<PaginatedResult<TEntity>> GetPaginatedAsync(
    int pageNumber,
    int pageSize,
    bool includeCount,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default);
}
