using RacksStand.Domain.Entities;

namespace RacksStand.Abstraction.Repositories;

public interface IRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : IEntity
{

}
