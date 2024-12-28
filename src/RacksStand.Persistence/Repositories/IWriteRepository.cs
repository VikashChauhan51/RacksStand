using RacksStand.Persistence.Entities;

namespace RacksStand.Persistence.Repositories;
public interface IWriteRepository<in TEntity, in TId> where TEntity : IEntity<TId> where TId : notnull
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}
