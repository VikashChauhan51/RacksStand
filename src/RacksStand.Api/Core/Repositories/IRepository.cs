using RacksStand.Api.Core.Domain.Entities;

namespace RacksStand.Api.Core.Repositories;

public interface IRepository<TEntity, TId>
    : IReadRepository<TEntity, TId>,
      IWriteRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{

}
