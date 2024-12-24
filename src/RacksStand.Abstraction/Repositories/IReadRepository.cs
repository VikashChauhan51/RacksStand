using RacksStand.Domain.Entities;

namespace RacksStand.Abstraction.Repositories;
public interface IReadRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull;
}
