using RacksStand.Domain.Entities;
using RacksStand.Models;
using System.Linq.Expressions;

namespace RacksStand.Persistence.Repositories;
public interface IReadRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull;
    Task<PaginatedResult<TEntity>> GetPaginatedAsync(
    int pageNumber,
    int pageSize,
    bool includeCount,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default);
}
