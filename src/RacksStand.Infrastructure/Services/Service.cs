using Microsoft.EntityFrameworkCore;
using RacksStand.Abstraction.Repositories;
using RacksStand.Abstraction.Services;
using RacksStand.Domain.Entities;
using RacksStand.Models;
using System.Linq.Expressions;

namespace RacksStand.Infrastructure.Services;
public class Service<TEntity, TId> : IService<TEntity, TId>
    where TEntity : class, IEntity
    where TId : notnull
{
    protected readonly IRepository<TEntity> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAll().ToListAsync(cancellationToken);
    }
    public async Task<PaginatedResult<TEntity>> GetPaginatedAsync(
    int pageNumber,
    int pageSize,
    bool includeCount,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _repository.GetAll();

        // Apply the predicate if provided
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Get the total item count if required
        int totalItems = 0;
        if (includeCount)
        {
            totalItems = await query.CountAsync(cancellationToken);
        }

        // Apply ordering if provided
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<TEntity>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        _repository.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
