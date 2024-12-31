using RacksStand.Domain.Core;

namespace RacksStand.Core.Repositories.Core;
public interface IRepository<TEntity, TId>
    : IReadRepository<TEntity, TId>, IWriteRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{

}
