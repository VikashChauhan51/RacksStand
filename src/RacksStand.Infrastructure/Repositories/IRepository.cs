using RacksStand.Domain.Core;

namespace RacksStand.Api.Core.Infrastructure.Repositories;

public interface IRepository<TEntity, TId>
    : IReadRepository<TEntity, TId>,
      IWriteRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{

}
