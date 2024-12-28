using RacksStand.Persistence.Entities;

namespace RacksStand.Persistence.Repositories;

public interface IRepository<TEntity, TId>
    : IReadRepository<TEntity, TId>,
      IWriteRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{

}
