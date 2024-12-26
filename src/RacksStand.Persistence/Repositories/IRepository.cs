using RacksStand.Domain.Entities;

namespace RacksStand.Persistence.Repositories;

public interface IRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : IEntity
{

}
